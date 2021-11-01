using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jobweb.Models
{
    public class PuestoTrabajo
    {
        public int id { get; set; }
        [Display(Name = "Compañia")]
        public int idCompañia { get; set; }
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
        [Display(Name = "Posicion")]
        public string posicion { get; set; }
        [Display(Name = "Ubicacion")]
        public string ubicacion { get; set; }
        [Display(Name = "Categoria")]
        public int idCategoria { get; set; }
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }
        [Display(Name = "Como Aplicar")]
        public string aplicar { get; set; }
        [Display(Name = "Estado")]
        public bool estado { get; set; }
        [Display(Name = "Fecha de Publicacion")]
        public DateTime fechaPublicacion { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
