using System;
using System.Net.Sockets;
using System.Text;

namespace MQClient
{
    public class MQClient
    {
        private string ip;
        private int port;
        private Guid appID;

        public MQClient(string ip, int port, Guid appID)
        {
            this.ip = ip;
            this.port = port;
            this.appID = appID;
        }

        public bool Subscribe(Topic topic)
        {
            try
            {
                // Crear la petición en formato: "Subscribe|AppID|Tema"
                string peticion = $"Subscribe|{appID}|{topic.Nombre}";

                // Enviar la petición al MQBroker
                string respuesta = EnviarPeticion(peticion);

                // Verificar la respuesta
                return respuesta == "Subscribed";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al suscribirse: {ex.Message}");
                return false;
            }
        }

        public bool Unsubscribe(Topic topic)
        {
            try
            {
                // Crear la petición en formato: "Unsubscribe|AppID|Tema"
                string peticion = $"Unsubscribe|{appID}|{topic.Nombre}";

                // Enviar la petición al MQBroker
                string respuesta = EnviarPeticion(peticion);

                // Verificar la respuesta
                return respuesta == "Unsubscribed";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al desuscribirse: {ex.Message}");
                return false;
            }
        }

        public bool Publish(Message message, Topic topic)
        {
            try
            {
                // Crear la petición en formato: "Publish|Tema|Contenido"
                string peticion = $"Publish|{topic.Nombre}|{message.Contenido}";

                // Enviar la petición al MQBroker
                string respuesta = EnviarPeticion(peticion);

                // Verificar la respuesta
                return respuesta == "Published";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al publicar: {ex.Message}");
                return false;
            }
        }

        public Message Receive(Topic topic)
        {
            try
            {
                // Crear la petición en formato: "Receive|AppID|Tema"
                string peticion = $"Receive|{appID}|{topic.Nombre}";

                // Enviar la petición al MQBroker
                string respuesta = EnviarPeticion(peticion);

                // Verificar si se recibió un mensaje
                if (!string.IsNullOrEmpty(respuesta))
                {
                    return new Message(respuesta);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recibir mensaje: {ex.Message}");
                return null;
            }
        }

        private string EnviarPeticion(string peticion)
        {
            // Crear un socket para conectarse al MQBroker
            using (TcpClient client = new TcpClient(ip, port))
            {
                NetworkStream stream = client.GetStream();

                // Enviar la petición al MQBroker
                byte[] buffer = Encoding.UTF8.GetBytes(peticion);
                stream.Write(buffer, 0, buffer.Length);

                // Recibir la respuesta del MQBroker
                buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer, 0, bytesRead);
            }
        }
    }
}