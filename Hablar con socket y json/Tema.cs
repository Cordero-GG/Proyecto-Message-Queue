using System.Collections.Generic;

namespace MQBroker
{
    public class Tema
    {
        public string Nombre { get; }
        private MiLista<Suscriptor> suscriptores;

        public Tema(string nombre)
        {
            Nombre = nombre;
            suscriptores = new MiLista<Suscriptor>();
        }

        public bool ContieneSuscriptor(Guid appID)
        {
            for (int i = 0; i < suscriptores.Count; i++)
            {
                if (suscriptores.Obtener(i).AppID == appID)
                {
                    return true;
                }
            }
            return false;
        }

        public void AgregarSuscriptor(Suscriptor suscriptor)
        {
            suscriptores.Agregar(suscriptor);
        }

        public void EliminarSuscriptor(Guid appID)
        {
            for (int i = 0; i < suscriptores.Count; i++)
            {
                if (suscriptores.Obtener(i).AppID == appID)
                {
                    suscriptores.Eliminar(i);
                    break;
                }
            }
        }

        public void PublicarMensaje(string contenido)
        {
            for (int i = 0; i < suscriptores.Count; i++)
            {
                suscriptores.Obtener(i).EncolarMensaje(contenido);
            }
        }

        public string ObtenerMensaje(Guid appID)
        {
            for (int i = 0; i < suscriptores.Count; i++)
            {
                if (suscriptores.Obtener(i).AppID == appID)
                {
                    return suscriptores.Obtener(i).DesencolarMensaje();
                }
            }
            return null;
        }
    }
}