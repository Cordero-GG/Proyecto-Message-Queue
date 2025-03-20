using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Hablar_con_socket_y_json
{ 
    public static class Publisher
    {
        public static async Task Start()
        {
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
}