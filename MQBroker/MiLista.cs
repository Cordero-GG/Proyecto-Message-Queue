namespace MQBroker
{
    // Clase genérica que implementa una lista dinámica personalizada
    public class MiLista<T>
    {
        // Array interno para almacenar los elementos de la lista
        private T[] elementos;

        // Capacidad actual del array (número máximo de elementos que puede contener antes de redimensionarse)
        private int capacidad;

        // Número actual de elementos almacenados en la lista
        private int count;

        // Constructor: inicializa la lista con una capacidad predeterminada
        public MiLista()
        {
            capacidad = 10; // Capacidad inicial de 10 elementos
            elementos = CrearArray(capacidad); // Crear un array vacío con la capacidad inicial
            count = 0; // Inicialmente, la lista está vacía
        }

        // Método para agregar un elemento a la lista
        public void Agregar(T elemento)
        {
            // Si el array está lleno, se redimensiona para duplicar su capacidad
            if (count == capacidad)
            {
                capacidad *= 2; // Duplicar la capacidad
                elementos = RedimensionarArray(elementos, capacidad); // Crear un nuevo array más grande y copiar los elementos
            }

            // Agregar el nuevo elemento al final de la lista
            elementos[count] = elemento;
            count++; // Incrementar el contador de elementos
        }

        // Método para obtener un elemento de la lista por su índice
        public T Obtener(int indice)
        {
            // Validar que el índice esté dentro de los límites de la lista
            if (indice < 0 || indice >= count)
            {
                throw new IndexOutOfRangeException(); // Lanzar una excepción si el índice es inválido
            }

            // Retornar el elemento en la posición especificada
            return elementos[indice];
        }

        // Método para eliminar un elemento de la lista por su índice
        public void Eliminar(int indice)
        {
            // Validar que el índice esté dentro de los límites de la lista
            if (indice < 0 || indice >= count)
            {
                throw new IndexOutOfRangeException(); // Lanzar una excepción si el índice es inválido
            }

            // Desplazar los elementos hacia la izquierda para llenar el espacio del elemento eliminado
            for (int i = indice; i < count - 1; i++)
            {
                elementos[i] = elementos[i + 1];
            }

            // Reducir el contador de elementos
            count--;
        }

        // Propiedad de solo lectura que devuelve el número actual de elementos en la lista
        public int Count => count;

        // Método auxiliar para crear un array vacío de un tamaño específico
        private T[] CrearArray(int tamaño)
        {
            // Crear un nuevo array con el tamaño especificado
            T[] nuevoArray = new T[tamaño];

            // Inicializar todos los elementos del array con el valor predeterminado del tipo T
            for (int i = 0; i < tamaño; i++)
            {
                nuevoArray[i] = default(T);
            }

            return nuevoArray; // Retornar el array creado
        }

        // Método auxiliar para redimensionar un array a un nuevo tamaño
        private T[] RedimensionarArray(T[] arrayOriginal, int nuevoTamaño)
        {
            // Crear un nuevo array con el tamaño especificado
            T[] nuevoArray = CrearArray(nuevoTamaño);

            // Copiar los elementos del array original al nuevo array
            for (int i = 0; i < count; i++)
            {
                nuevoArray[i] = arrayOriginal[i];
            }

            return nuevoArray; // Retornar el nuevo array redimensionado
        }
    }
}
