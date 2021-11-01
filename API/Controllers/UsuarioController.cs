using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private JobWebDB db = new JobWebDB();
        // GET: api/Usuario
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
           
                return db.Usuario.AsEnumerable().Select(x => new Usuario
                {
                    id = x.id,
                    username = x.username,
                    tipo = x.tipo
                }).ToList();
            
                

        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public Usuario Get(int id, string user = null)
        {
            if(user == null)
                return db.Usuario.Find(id);
            else
                return (from usr in db.Usuario
                        where usr.username == user
                        select usr).FirstOrDefault();
        }

        // POST: api/Usuario
        [HttpPost]
        public void Post([FromBody] Usuario value)
        {
            db.Usuario.Add(value);
            db.SaveChanges();
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Usuario value)
        {
            value.id = id;
            db.Usuario.Update(value);
            db.SaveChanges();
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Usuario value = db.Usuario.Find(id);
            db.Usuario.Remove(value);
            db.SaveChanges();
        }
    }
}
