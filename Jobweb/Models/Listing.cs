using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jobweb.Models
{
    public class Listing
    {
        public int id { get; set; }
        public string company { get; set; }
        public string categoria { get; set; }
        public string tipo { get; set; }
        public string posicion { get; set; }
        public string ubicacion { get; set; }
        public string logo { get; set; }
        public string descripcion { get; set; }
        public string aplicar { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public DateTime fechaPublicacion { get; set; }
    }
}