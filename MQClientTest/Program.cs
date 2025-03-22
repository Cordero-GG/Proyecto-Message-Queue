using System;
using MQClient;

namespace MQClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crear un cliente MQClient
            Guid appID = Guid.NewGuid();
            MQClient.MQClient client = new MQClient.MQClient("127.0.0.1", 5000, appID);

            // Crear un tema
            Topic tema = new Topic("Tema1");

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n--- Menú ---");
                Console.WriteLine("1. Suscribirse");
                Console.WriteLine("2. Publicar mensaje");
                Console.WriteLine("3. Recibir mensaje");
                Console.WriteLine("4. Desuscribirse");
                Console.WriteLine("5. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": // Suscribirse
                        bool suscrito = client.Subscribe(tema);
                        Console.WriteLine(suscrito ? "Suscrito correctamente." : "Error al suscribirse.");
                        break;

                    case "2": // Publicar mensaje
                        Console.Write("Ingrese el mensaje: ");
                        string contenido = Console.ReadLine();
                        Message mensaje = new Message(contenido);
                        bool publicado = client.Publish(mensaje, tema);
                        Console.WriteLine(publicado ? "Mensaje publicado correctamente." : "Error al publicar el mensaje.");
                        break;

                    case "3": // Recibir mensaje
                        Message mensajeRecibido = client.Receive(tema);
                        if (mensajeRecibido != null)
                        {
                            Console.WriteLine($"Mensaje recibido: {mensajeRecibido.Contenido}");
                        }
                        else
                        {
                            Console.WriteLine("No hay mensajes disponibles.");
                        }
                        break;

                    case "4": // Desuscribirse
                        bool desuscrito = client.Unsubscribe(tema);
                        Console.WriteLine(desuscrito ? "Desuscrito correctamente." : "Error al desuscribirse.");
                        break;

                    case "5": // Salir
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }

            Console.WriteLine("Saliendo...");
        }
    }
}