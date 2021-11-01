using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class PuestoTrabajo
    {
        public int id { get; set; }
        public int idCompañia { get; set; }
        public string tipo { get; set; }
        public string posicion { get; set; }
        public string ubicacion { get; set; }
        public int idCategoria { get; set; }
        public string descripcion { get; set; }
        public string aplicar { get; set; }
        public bool estado { get; set; }
        public DateTime fechaPublicacion { get; set; }

       
    }
}
