using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Jobweb.Models
{
    public class Categoria
    {
        public int id { get; set; }
        [Display(Name = "Categoria")]
        public string categoria { get; set; }
        [Display(Name = "Disponibilidad")]
        public int disponibilidad { get; set; }
    }
}
