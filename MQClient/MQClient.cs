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
        private TcpClient client;
        private NetworkStream stream;

        public MQClient(string ip, int port, Guid appID)
        {
            this.ip = ip;
            this.port = port;
            this.appID = appID;
            this.client = new TcpClient(ip, port);
            this.stream = client.GetStream();
        }

        public bool Subscribe(Topic topic)
        {
            try
            {
                string peticion = $"Subscribe|{appID}|{topic.Nombre}";
                string respuesta = EnviarPeticion(peticion);
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
                string peticion = $"Unsubscribe|{appID}|{topic.Nombre}";
                string respuesta = EnviarPeticion(peticion);
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
                string peticion = $"Publish|{topic.Nombre}|{message.Contenido}";
                string respuesta = EnviarPeticion(peticion);
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
                string peticion = $"Receive|{appID}|{topic.Nombre}";
                string respuesta = EnviarPeticion(peticion);
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
            byte[] buffer = Encoding.UTF8.GetBytes(peticion);
            stream.Write(buffer, 0, buffer.Length);

            buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        public void CerrarConexion()
        {
            stream.Close();
            client.Close();
        }
    }
}
