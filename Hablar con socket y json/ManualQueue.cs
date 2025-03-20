using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hablar_con_socket_y_json
{
    public class ManualQueue<T>
    {
        private readonly List<T> _items = new List<T>();

        public void Enqueue(T item) => _items.Add(item);
        public T Dequeue()
        {
            if (_items.Count == 0) throw new InvalidOperationException("Cola vacía");
            T item = _items[0];
            _items.RemoveAt(0);
            return item;
        }
        public bool HasItems => _items.Count > 0;
    }
}
