namespace MQBroker
{
    public class Tema
    {
        public string Nombre { get; }
        private readonly MiLista<Suscriptor> _suscriptores = new MiLista<Suscriptor>();

        public Tema(string nombre)
        {
            Nombre = nombre;
        }

        public bool ContieneSuscriptor(Guid appId)
        {
            for (int i = 0; i < _suscriptores.Count; i++)
            {
                if (_suscriptores.Obtener(i).AppID == appId)
                    return true;
            }
            return false;
        }

        public void AgregarSuscriptor(Suscriptor suscriptor)
        {
            _suscriptores.Agregar(suscriptor);
        }

        public void EliminarSuscriptor(Guid appId)
        {
            for (int i = 0; i < _suscriptores.Count; i++)
            {
                if (_suscriptores.Obtener(i).AppID == appId)
                {
                    _suscriptores.Eliminar(i);
                    break;
                }
            }
        }

        public void PublicarMensaje(string contenido)
        {
            for (int i = 0; i < _suscriptores.Count; i++)
            {
                _suscriptores.Obtener(i).EncolarMensaje(contenido);
            }
        }

        public string ObtenerMensaje(Guid appId)
        {
            for (int i = 0; i < _suscriptores.Count; i++)
            {
                if (_suscriptores.Obtener(i).AppID == appId)
                {
                    return _suscriptores.Obtener(i).DesencolarMensaje() ?? "ERROR|No hay mensajes";
                }
            }
            return "ERROR|No suscrito al tema";
        }
    }
}