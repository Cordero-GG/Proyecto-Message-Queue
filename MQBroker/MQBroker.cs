//MQBroker.cs
using System;
using System.Collections.Generic;

namespace MQBroker
{
    public class MQBroker
    {
        private readonly MiLista<Tema> _temas = new MiLista<Tema>();

        public void Subscribe(Guid appId, string nombreTema)
        {
            Tema tema = BuscarTema(nombreTema) ?? new Tema(nombreTema);
            if (!ContieneTema(tema))
            {
                tema.AgregarSuscriptor(new Suscriptor(appId));
                if (!ContieneTema(tema))
                    _temas.Agregar(tema);
            }
        }

        public void Unsubscribe(Guid appId, string nombreTema)
        {
            Tema? tema = BuscarTema(nombreTema);
            tema?.EliminarSuscriptor(appId);
        }

        public void Publish(string nombreTema, string contenido)
        {
            Tema? tema = BuscarTema(nombreTema);
            tema?.PublicarMensaje(contenido);
        }

        public string Receive(Guid appId, string nombreTema)
        {
            Tema? tema = BuscarTema(nombreTema);
            return tema?.ObtenerMensaje(appId) ?? "ERROR|Tema no encontrado";
        }

        private Tema? BuscarTema(string nombre)
        {
            for (int i = 0; i < _temas.Count; i++)
            {
                if (_temas.Obtener(i).Nombre == nombre)
                    return _temas.Obtener(i);
            }
            return null;
        }

        private bool ContieneTema(Tema tema)
        {
            for (int i = 0; i < _temas.Count; i++)
            {
                if (_temas.Obtener(i).Nombre == tema.Nombre)
                    return true;
            }
            return false;
        }
    }
}
