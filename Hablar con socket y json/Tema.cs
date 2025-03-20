using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hablar_con_socket_y_json
{
    public class Tema
    {
        public string Nombre { get; set; }
        public List<Suscriptor> Suscriptores { get; set; } = new List<Suscriptor>();
    }
}
