using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hablar_con_socket_y_json
{
    public class Suscriptor
    {
        public string Nombre { get; set; }
        public ManualQueue<Message> Mensajes { get; set; } = new ManualQueue<Message>();
    }
}
