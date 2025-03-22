using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQBroker
{
    public class Suscriptor
    {
        public Guid AppID { get; }
        private MiCola<string> colaMensajes;

        public Suscriptor(Guid appID)
        {
            AppID = appID;
            colaMensajes = new MiCola<string>();
        }

        public void EncolarMensaje(string mensaje)
        {
            colaMensajes.Enqueue(mensaje);
        }

        public string DesencolarMensaje()
        {
            if (colaMensajes.Count > 0)
            {
                return colaMensajes.Dequeue();
            }
            return null;
        }
    }
}
