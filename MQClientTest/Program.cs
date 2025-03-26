using System;
using MQClient;

namespace MQClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configuración del cliente
            string ip = "127.0.0.1"; // IP del servidor (MQBroker)
            int port = 5000;         // Puerto del servidor (MQBroker)
            Guid appID = Guid.NewGuid(); // AppID único para este cliente

            // Crear una instancia del MQClient
            MQClient.MQClient client = new MQClient.MQClient(ip, port, appID);

            // Crear un tema
            Topic tema = new Topic("Tema1");

            // 1. Suscribirse al tema
            bool suscrito = client.Subscribe(tema);
            Console.WriteLine(suscrito ? "Suscrito correctamente." : "Error al suscribirse.");

            // 2. Publicar un mensaje en el tema
            Message mensaje = new Message("Hola, este es un mensaje de prueba.");
            bool publicado = client.Publish(mensaje, tema);
            Console.WriteLine(publicado ? "Mensaje publicado correctamente." : "Error al publicar el mensaje.");

            // 3. Recibir un mensaje del tema
            Message mensajeRecibido = client.Receive(tema);
            if (mensajeRecibido != null)
            {
                Console.WriteLine($"Mensaje recibido: {mensajeRecibido.Contenido}");
            }
            else
            {
                Console.WriteLine("No hay mensajes disponibles.");
            }

            // 4. Desuscribirse del tema
            bool desuscrito = client.Unsubscribe(tema);
            Console.WriteLine(desuscrito ? "Desuscrito correctamente." : "Error al desuscribirse.");

            Console.WriteLine("Prueba completada. Presiona cualquier tecla para salir...");
            Console.ReadKey();

            // Al final del método Main
            client.CerrarConexion();
            Console.WriteLine("Prueba completada. Presiona cualquier tecla para salir...");
            Console.ReadKey();

        }
    }
}