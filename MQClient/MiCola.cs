using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQClient
{
    public class MiCola<T>
    {
        private T[] elementos;
        private int frente;
        private int final;
        private int count;

        public MiCola()
        {
            elementos = new T[10]; // Capacidad inicial
            frente = 0;
            final = -1;
            count = 0;
        }

        public void Enqueue(T elemento)
        {
            if (count == elementos.Length)
            {
                // Redimensionar el array si está lleno
                Array.Resize(ref elementos, elementos.Length * 2);
            }
            final = (final + 1) % elementos.Length;
            elementos[final] = elemento;
            count++;
        }

        public T Dequeue()
        {
            if (count == 0)
            {
                throw new InvalidOperationException("La cola está vacía");
            }
            T elemento = elementos[frente];
            frente = (frente + 1) % elementos.Length;
            count--;
            return elemento;
        }

        public int Count => count;
    }
}
