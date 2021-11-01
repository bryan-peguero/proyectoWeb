using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Jobweb.Models
{
    public class Usuario
    {
        public int id { get; set; }
        [Display(Name = "Username")]
        public string username { get; set; }
        public string password { get; set; }
        [Display(Name = "Tipo")]
        public string tipo { get; set; }
        
    }
}
