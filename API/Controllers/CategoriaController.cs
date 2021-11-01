using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.EntityFrameworkCore;
using API.Data;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private JobWebDB db = new JobWebDB();
        // GET: api/Categoria
        [HttpGet]
        public IEnumerable<Categoria> Get()
        {
            
            return db.Categoria.ToList();
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public Categoria Get(int id)
        {
            return db.Categoria.Find(id);
        }

        // POST: api/Categoria
        [HttpPost]
        public void Post([FromBody] Categoria value)
        {
            db.Categoria.Add(value);
            db.SaveChanges();
        }

        // PUT: api/Categoria/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Categoria value)
        {
            value.id = id;
            db.Categoria.Update(value);
            db.SaveChanges();
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Categoria value = db.Categoria.Find(id);
            db.Categoria.Remove(value);
            db.SaveChanges();
        }
    }
}
