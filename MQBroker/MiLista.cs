//MiLista.cs
namespace MQBroker
{
    public class MiLista<T>
    {
        private T[] elementos;
        private int capacidad;
        private int count;

        public MiLista()
        {
            capacidad = 10; // Capacidad inicial
            elementos = new T[capacidad];
            count = 0;
        }

        public void Agregar(T elemento)
        {
            if (count == capacidad)
            {
                // Redimensionar el array si está lleno
                capacidad *= 2;
                Array.Resize(ref elementos, capacidad);
            }
            elementos[count] = elemento;
            count++;
        }

        public T Obtener(int indice)
        {
            if (indice < 0 || indice >= count)
            {
                throw new IndexOutOfRangeException();
            }
            return elementos[indice];
        }

        public void Eliminar(int indice)
        {
            if (indice < 0 || indice >= count)
            {
                throw new IndexOutOfRangeException();
            }

            for (int i = indice; i < count - 1; i++)
            {
                elementos[i] = elementos[i + 1];
            }
            count--;
        }

        public int Count => count;
    }
}