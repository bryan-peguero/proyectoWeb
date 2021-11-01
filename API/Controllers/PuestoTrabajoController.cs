using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PuestoTrabajoController : ControllerBase
    {
        private JobWebDB db = new JobWebDB();
        // GET: api/PuestoTrabajo or GET: api/PuestoTrabajo?search=query
        [HttpGet]
        public IEnumerable<PuestoTrabajo> Get()
        {
            return db.PuestoTrabajo.ToList();
        }

        // GET: api/PuestoTrabajo/5
        [HttpGet("{id}")]
        public PuestoTrabajo Get(int id)
        {
            return db.PuestoTrabajo.Find(id);
        }

        // POST: api/PuestoTrabajo
        [HttpPost]
        public void Post([FromBody] PuestoTrabajo value)
        {
            db.PuestoTrabajo.Add(value);
            db.SaveChanges();
        }

        // PUT: api/PuestoTrabajo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PuestoTrabajo value)
        {
            value.id = id;
            db.PuestoTrabajo.Update(value);
            db.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            PuestoTrabajo value = db.PuestoTrabajo.Find(id);
            db.PuestoTrabajo.Remove(value);
            db.SaveChanges();
        }
    }
}
