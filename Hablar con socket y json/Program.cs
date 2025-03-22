using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MQBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configurar el servidor
            string ip = "127.0.0.1"; // IP local
            int port = 5000; // Puerto

            // Crear una instancia del MQBroker
            MQBroker broker = new MQBroker();

            // Iniciar el servidor para escuchar peticiones
            TcpListener server = new TcpListener(IPAddress.Parse(ip), port);
            server.Start();
            Console.WriteLine($"Servidor MQBroker iniciado en {ip}:{port}...");

            while (true)
            {
                // Esperar una conexión de un cliente
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Cliente conectado.");

                // Manejar la petición del cliente en un hilo separado
                ManejarCliente(broker, client);
            }
        }

        static void ManejarCliente(MQBroker broker, TcpClient client)
        {
            try
            {
                // Obtener el stream de red para leer y escribir datos
                NetworkStream stream = client.GetStream();

                // Leer los datos enviados por el cliente
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string peticion = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Petición recibida: {peticion}");

                // Interpretar la petición
                string[] partes = peticion.Split('|');
                string comando = partes[0];

                switch (comando)
                {
                    case "Subscribe":
                        {
                            Guid appID = Guid.Parse(partes[1]);
                            string tema = partes[2];
                            broker.Subscribe(appID, tema);
                            break;
                        }
                    case "Unsubscribe":
                        {
                            Guid appID = Guid.Parse(partes[1]);
                            string tema = partes[2];
                            broker.Unsubscribe(appID, tema);
                            break;
                        }
                    case "Publish":
                        {
                            string tema = partes[1];
                            string contenido = partes[2];
                            broker.Publish(tema, contenido);
                            break;
                        }
                    case "Receive":
                        {
                            Guid appID = Guid.Parse(partes[1]);
                            string tema = partes[2];
                            string mensaje = broker.Receive(appID, tema);
                            if (mensaje != null)
                            {
                                // Enviar el mensaje de vuelta al cliente
                                byte[] respuesta = Encoding.UTF8.GetBytes(mensaje);
                                stream.Write(respuesta, 0, respuesta.Length);
                            }
                            break;
                        }
                    default:
                        Console.WriteLine($"Comando no reconocido: {comando}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al manejar la petición del cliente: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión con el cliente
                client.Close();
                Console.WriteLine("Cliente desconectado.");
            }
        }
    }
}