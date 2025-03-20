using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hablar_con_socket_y_json
{
    public static class Subscriber
    {
        private static readonly List<Tema> Temas = new List<Tema>();

        public static async Task Start()
        {
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
            string content = messageBuilder.ToString();
            int eomIndex;

            while ((eomIndex = content.IndexOf("<|EOM|>")) != -1)
            {
                string json = content.Substring(0, eomIndex);
                content = content[(eomIndex + 7)..]; // 7 = longitud de <|EOM|>

                try
                {
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

}
