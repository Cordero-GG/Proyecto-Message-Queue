using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace MQBroker
{
    public class MQBroker
    {
        private MiLista<Tema> temas;

        public MQBroker()
        {
            temas = new MiLista<Tema>();
        }

        public void ManejarCliente(TcpClient client)
        {
            // Aquí se procesan las peticiones del cliente (Subscribe, Unsubscribe, Publish, Receive)
            // Este método debe leer los datos del socket, interpretar la petición y llamar al método correspondiente.
            // Por simplicidad, este es un esqueleto básico.
            Console.WriteLine("Procesando petición del cliente...");
        }

        public void Subscribe(Guid appID, string tema)
        {
            // Verificar si el tema ya existe
            Tema t = BuscarTema(tema);
            if (t == null)
            {
                // Si no existe, crear un nuevo tema
                t = new Tema(tema);
                temas.Agregar(t);
            }

            // Verificar si el AppID ya está suscrito
            if (!t.ContieneSuscriptor(appID))
            {
                // Si no está suscrito, agregarlo
                t.AgregarSuscriptor(new Suscriptor(appID));
                Console.WriteLine($"AppID {appID} suscrito al tema {tema}.");
            }
            else
            {
                Console.WriteLine($"AppID {appID} ya está suscrito al tema {tema}.");
            }
        }

        public void Unsubscribe(Guid appID, string tema)
        {
            // Buscar el tema
            Tema t = BuscarTema(tema);
            if (t != null)
            {
                // Eliminar el suscriptor
                t.EliminarSuscriptor(appID);
                Console.WriteLine($"AppID {appID} eliminado del tema {tema}.");
            }
            else
            {
                Console.WriteLine($"Tema {tema} no encontrado.");
            }
        }

        public void Publish(string tema, string contenido)
        {
            // Buscar el tema
            Tema t = BuscarTema(tema);
            if (t != null)
            {
                // Publicar el mensaje a todos los suscriptores
                t.PublicarMensaje(contenido);
                Console.WriteLine($"Mensaje publicado en el tema {tema}.");
            }
            else
            {
                Console.WriteLine($"Tema {tema} no encontrado.");
            }
        }

        public string Receive(Guid appID, string tema)
        {
            // Buscar el tema
            Tema t = BuscarTema(tema);
            if (t != null)
            {
                // Obtener el mensaje para el suscriptor
                string mensaje = t.ObtenerMensaje(appID);
                if (mensaje != null)
                {
                    Console.WriteLine($"Mensaje entregado a AppID {appID} en el tema {tema}.");
                    return mensaje;
                }
                else
                {
                    Console.WriteLine($"No hay mensajes para AppID {appID} en el tema {tema}.");
                    return null;
                }
            }
            else
            {
                Console.WriteLine($"Tema {tema} no encontrado.");
                return null;
            }
        }

        private Tema BuscarTema(string nombre)
        {
            // Buscar un tema por nombre
            for (int i = 0; i < temas.Count; i++)
            {
                if (temas.Obtener(i).Nombre == nombre)
                {
                    return temas.Obtener(i);
                }
            }
            return null;
        }
    }
}