//Topic.cs
using System;
using System.Net;
using System.Net.Sockets;

namespace MQClient
{
    public class Topic
    {
        public string Nombre { get; }

        public Topic(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El nombre del tema no puede ser nulo o vacío.");
            }
            Nombre = nombre;
        }
    }
}