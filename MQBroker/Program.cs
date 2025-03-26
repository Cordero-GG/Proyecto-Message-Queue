using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MQBroker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Uso: MQBroker <IP> <Puerto>");
                return;
            }

            string ip = args[0];
            int port = int.Parse(args[1]);
            MQBroker broker = new MQBroker();

            try
            {
                TcpListener server = new TcpListener(IPAddress.Parse(ip), port);
                Console.WriteLine($"Intentando iniciar servidor en {ip}:{port}...");
                server.Start();
                Console.WriteLine($"Servidor MQBroker iniciado correctamente en {ip}:{port}...");
                Console.WriteLine($"Endpoint local: {server.LocalEndpoint}");

                while (true)
                {
                    Console.WriteLine("Esperando conexiones...");
                    TcpClient client = await server.AcceptTcpClientAsync();
                    Console.WriteLine("Cliente conectado.");
                    _ = Task.Run(() => ManejarCliente(broker, client));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar el servidor: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        static async Task ManejarCliente(MQBroker broker, TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true })
                {
                    while (client.Connected)
                    {
                        string peticion = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(peticion)) break;

                        Console.WriteLine($"Petición recibida: {peticion}");
                        string respuesta = ProcesarPeticion(broker, peticion);

                        await writer.WriteLineAsync(respuesta);
                        Console.WriteLine($"Respuesta enviada: {respuesta}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al manejar el cliente: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Cliente desconectado.");
            }
        }

        static string ProcesarPeticion(MQBroker broker, string peticion)
        {
            try
            {
                string[] partes = peticion.Split('|');
                switch (partes[0])
                {
                    case "Subscribe":
                        broker.Subscribe(Guid.Parse(partes[1]), partes[2]);
                        return "OK|Subscribed";
                    case "Unsubscribe":
                        broker.Unsubscribe(Guid.Parse(partes[1]), partes[2]);
                        return "OK|Unsubscribed";
                    case "Publish":
                        broker.Publish(partes[1], partes[2]);
                        return "OK|Published";
                    case "Receive":
                        string mensaje = broker.Receive(Guid.Parse(partes[1]), partes[2]);
                        return $"OK|{mensaje}";
                    default:
                        return "ERROR|Comando inválido";
                }
            }
            catch (Exception ex)
            {
                return $"ERROR|{ex.Message}";
            }
        }
    }
}
