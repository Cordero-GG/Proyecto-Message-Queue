using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hablar_con_socket_y_json
{
    public class Message
    {
        public required string Nombre { get; set; }
        public required string Tema { get; set; }
        public required string Contenido { get; set; }
        public required string IP { get; set; }
    }
}
