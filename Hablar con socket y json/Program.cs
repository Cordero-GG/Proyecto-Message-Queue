using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Sistema Pub/Sub Continuo\n");

        // Iniciar Subscriber en segundo plano
        var subscriberTask = Task.Run(Subscriber.Start);

        // Esperar 1 segundo para que el Subscriber inicie
        await Task.Delay(1000);

        // Iniciar Publisher interactivo
        await Publisher.Start();

        await subscriberTask; // Mantener la app activa
    }
}



// ================= SUBSYSTEM =================
public static class Subscriber
{
    // Se crea la lista "Temas" para guardar los temas y suscriptores
    private static readonly List<Tema> Temas = new List<Tema>();

    public static async Task Start()
    {
        // Se toma la dirección IP para conectarse al servidor
        var ipEndPoint = new IPEndPoint(IPAddress.Any, 9000);
        using var listener = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        listener.Bind(ipEndPoint);
        listener.Listen(10);
        Console.WriteLine("[Subscriber] Escuchando en puerto 9000...");

        using var handler = await listener.AcceptAsync();
        Console.WriteLine("[Subscriber] Cliente conectado!");

        var buffer = new byte[4096];
        var messageBuilder = new StringBuilder();

        try
        {
            while (true)
            {
                int received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                if (received == 0) break; // Cliente desconectado

                messageBuilder.Append(Encoding.UTF8.GetString(buffer, 0, received));

                ProcessMessages(handler, messageBuilder);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Subscriber Error] {ex.Message}");
        }
    }

    private static void ProcessMessages(Socket handler, StringBuilder messageBuilder)
    {
        // método encargado de tomar el mensaje y hacer de string a JSON
        string content = messageBuilder.ToString();
        int eomIndex;

        while ((eomIndex = content.IndexOf("<|EOM|>")) != -1)
        {
            string json = content.Substring(0, eomIndex);
            content = content[(eomIndex + 7)..]; // 7 = longitud de <|EOM|>

            try
            {
                //variables del jsom
                var message = JsonSerializer.Deserialize<Message>(json);
                Console.WriteLine($"[Subscriber] Mensaje recibido:");
                Console.WriteLine($"- Nombre: {message.Nombre}");
                Console.WriteLine($"- Tema: {message.Tema}");
                Console.WriteLine($"- Contenido: {message.Contenido}");
                Console.WriteLine($"- IP: {message.IP}");

                // Guardar el mensaje en la cola del suscriptor correspondiente
                GuardarMensaje(message);
            }
            catch (JsonException)
            {
                Console.WriteLine("[Subscriber] Error: JSON inválido recibido.");
            }

            // Enviar ACK
            handler.Send(Encoding.UTF8.GetBytes("<|ACK|>"));
        }

        messageBuilder.Clear();
        messageBuilder.Append(content);
    }

    private static void GuardarMensaje(Message message)
    {
        // Buscar el tema
        var tema = Temas.Find(t => t.Nombre == message.Tema);
        if (tema == null)
        {
            // Si el tema no existe, crearlo
            tema = new Tema { Nombre = message.Tema };
            Temas.Add(tema);
        }

        // Buscar el suscriptor
        var suscriptor = tema.Suscriptores.Find(s => s.Nombre == message.Nombre);
        if (suscriptor == null)
        {
            // Si el suscriptor no existe, crearlo
            suscriptor = new Suscriptor { Nombre = message.Nombre };
            tema.Suscriptores.Add(suscriptor);
        }

        // Encolar el mensaje
        suscriptor.Mensajes.Enqueue(message);
    }
}

// ================= PUBSYSTEM =================
public static class Publisher
{
    public static async Task Start()
    {
        // Conectar al servidor
        var ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000);
        using var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        await socket.ConnectAsync(ipEndPoint);
        Console.WriteLine("[Publisher] Conectado al servidor!");

        while (true)
        {
            Console.Write("\nIngrese un JSON (ej: {\"Nombre\":\"Juan\",\"Tema\":\"Ciencia\",\"Contenido\":\"Los planetas y las estrellas\",\"IP\":\"127.0.0.1\"}) o 'salir': ");
            string input = Console.ReadLine();

            if (input?.ToLower() == "salir") break;

            if (IsValidJson(input))
            {
                await SendMessage(socket, input);
            }
            else
            {
                Console.WriteLine("[Publisher] Error: JSON inválido. Inténtelo de nuevo.");
            }
        }
    }

    private static bool IsValidJson(string input)
    {
        // Verificar si el input es un JSON válido
        try
        {
            JsonSerializer.Deserialize<Message>(input);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }

    private static async Task SendMessage(Socket socket, string json)
    {
        string message = json + "<|EOM|>";
        byte[] bytes = Encoding.UTF8.GetBytes(message);

        await socket.SendAsync(bytes, SocketFlags.None);
        Console.WriteLine("[Publisher] Mensaje enviado, esperando ACK...");

        // Esperar confirmación
        var ackBuffer = new byte[1024];
        int received = await socket.ReceiveAsync(ackBuffer, SocketFlags.None);
        Console.WriteLine($"[Publisher] Respuesta: {Encoding.UTF8.GetString(ackBuffer, 0, received)}");
    }
}

public class Tema
{
    public string Nombre { get; set; }
    public List<Suscriptor> Suscriptores { get; set; } = new List<Suscriptor>();
}

public class Suscriptor
{
    public string Nombre { get; set; }
    public ManualQueue<Message> Mensajes { get; set; } = new ManualQueue<Message>();
}

public class ManualQueue<T>
{
    private readonly List<T> _items = new List<T>();

    public void Enqueue(T item) => _items.Add(item);
    public T Dequeue()
    {
        if (_items.Count == 0) throw new InvalidOperationException("Cola vacía");
        T item = _items[0];
        _items.RemoveAt(0);
        return item;
    }
    public bool HasItems => _items.Count > 0;
}

// Nueva clase Message con los campos solicitados
public class Message
{
    public required string Nombre { get; set; }
    public required string Tema { get; set; }
    public required string Contenido { get; set; }
    public required string IP { get; set; }
}