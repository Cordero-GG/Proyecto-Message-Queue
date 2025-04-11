namespace MQBroker
{
    public class MiCola<T>
    {
        private T[] elementos; // Array para almacenar los elementos
        private int frente;
        private int final;
        private int count;

        public MiCola()
        {
            elementos = new T[10]; // Inicializa con una capacidad inicial de 10
            frente = 0;
            final = -1;
            count = 0;
        }

        public void Enqueue(T elemento) // Agregar un elemento a la cola
        {
            if (count == elementos.Length)
            {
                // Redimensionar manualmente el array si está lleno
                Redimensionar();
            }
            final = (final + 1) % elementos.Length;
            elementos[final] = elemento;
            count++;
        }

        public T Dequeue() // Sacar un elemento de la cola
        {
            if (count == 0)
            {
                throw new InvalidOperationException("La cola está vacía"); // Excepción si la cola está vacía
            }
            T elemento = elementos[frente]; // Obtener el elemento en la posición frente
            frente = (frente + 1) % elementos.Length;
            count--;
            return elemento;
        }

        public int Count
        {
            get { return count; }
        }

        private void Redimensionar()
        {
            int nuevaCapacidad = elementos.Length * 2;
            T[] nuevoArray = new T[nuevaCapacidad];

            // Copiar los elementos al nuevo array en el orden correcto
            for (int i = 0; i < count; i++)
            {
                nuevoArray[i] = elementos[(frente + i) % elementos.Length];
            }

            elementos = nuevoArray;
            frente = 0;
            final = count - 1;
        }
    }
}
