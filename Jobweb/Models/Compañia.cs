using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobweb.Models
{
    public class Compañia
    {
        public int id { get; set; }
        public string logo { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public string nombre { get; set; }
        public int idUsuario { get; set; }
    }
}
