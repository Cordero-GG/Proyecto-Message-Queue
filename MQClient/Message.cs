using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQClient
{
    public class Message
    {
        public string Contenido { get; }

        public Message(string contenido)
        {
            if (string.IsNullOrEmpty(contenido))
            {
                throw new ArgumentException("El contenido del mensaje no puede ser nulo o vacío.");
            }
            Contenido = contenido;
        }
    }
}
