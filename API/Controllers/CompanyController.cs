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
    public class CompanyController : ControllerBase
    {
        private JobWebDB db = new JobWebDB();
        // GET: api/Compañia
        [HttpGet]
        public IEnumerable<Compañia> Get()
        {
            return db.Compañia.ToList();
        }

        // GET: api/Compañia/5
        [HttpGet("{id}")]
        public Compañia Get(int id, int idUser = 0)
        {
            if(idUser == 0)
                return db.Compañia.Find(id);
            else 
                return (from cp in db.Compañia
                        where cp.idUsuario == idUser
                        select cp).FirstOrDefault();
        }

        // POST: api/Compañia
        [HttpPost]
        public void Post([FromBody] Compañia value)
        {
            db.Compañia.Add(value);
            db.SaveChanges();
        }

        // PUT: api/Compañia/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Compañia value)
        {
            value.id = id;
            db.Compañia.Update(value);
            db.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Compañia value = db.Compañia.Find(id);
            db.Compañia.Remove(value);
            db.SaveChanges();
        }
    }
}
