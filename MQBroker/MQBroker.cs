using System;
using System.Collections.Generic;

namespace MQBroker
{
    public class MQBroker
    {
        // Lista personalizada que almacena los temas disponibles en el broker.
        private readonly MiLista<Tema> _temas = new MiLista<Tema>();

        // Método para suscribir una aplicación (identificada por appId) a un tema específico.
        public void Subscribe(Guid appId, string nombreTema)
        {
            // Busca si el tema ya existe en la lista de temas.
            Tema tema = BuscarTema(nombreTema);
            if (tema == null)
            {
                // Si el tema no existe, se crea uno nuevo.
                tema = new Tema(nombreTema);
                // Se agrega un nuevo suscriptor al tema.
                tema.AgregarSuscriptor(new Suscriptor(appId));
                // Se agrega el tema a la lista de temas.
                _temas.Agregar(tema);
            }
            else
            {
                // Si el tema ya existe, verifica si el suscriptor ya está registrado.
                if (!tema.ContieneSuscriptor(appId))
                {
                    // Si no está registrado, lo agrega como suscriptor.
                    tema.AgregarSuscriptor(new Suscriptor(appId));
                }
            }
        }

        // Método para desuscribir una aplicación de un tema específico.
        public void Unsubscribe(Guid appId, string nombreTema)
        {
            // Busca el tema en la lista de temas.
            Tema? tema = BuscarTema(nombreTema);
            // Si el tema existe, elimina al suscriptor del tema.
            tema?.EliminarSuscriptor(appId);
        }

        // Método para publicar un mensaje en un tema específico.
        public void Publish(string nombreTema, string contenido)
        {
            // Busca el tema en la lista de temas.
            Tema? tema = BuscarTema(nombreTema);
            // Si el tema existe, publica el mensaje en el tema.
            tema?.PublicarMensaje(contenido);
        }

        // Método para que un suscriptor reciba un mensaje de un tema específico.
        public string Receive(Guid appId, string nombreTema)
        {
            // Busca el tema en la lista de temas.
            Tema? tema = BuscarTema(nombreTema);
            // Si el tema existe, intenta obtener un mensaje para el suscriptor.
            // Si no existe, devuelve un mensaje de error.
            return tema?.ObtenerMensaje(appId) ?? "ERROR|Tema no encontrado";
        }

        // Método privado para buscar un tema por su nombre en la lista de temas.
        private Tema? BuscarTema(string nombre)
        {
            // Recorre la lista de temas buscando uno con el nombre especificado.
            for (int i = 0; i < _temas.Count; i++)
            {
                if (_temas.Obtener(i).Nombre == nombre)
                    return _temas.Obtener(i);
            }
            // Si no encuentra el tema, devuelve null.
            return null;
        }

        // Método privado para verificar si un tema ya existe en la lista de temas.
        private bool ContieneTema(Tema tema)
        {
            // Recorre la lista de temas buscando uno con el mismo nombre.
            for (int i = 0; i < _temas.Count; i++)
            {
                if (_temas.Obtener(i).Nombre == tema.Nombre)
                    return true;
            }
            // Si no encuentra el tema, devuelve false.
            return false;
        }
    }
}
