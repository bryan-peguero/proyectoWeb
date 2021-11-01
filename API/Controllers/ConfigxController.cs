using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigxController : ControllerBase
    {
        JobWebDB db = new JobWebDB();
        // GET: api/<ConfigxController>
        [HttpGet]
        public IEnumerable<configx> Get()
        {
            return db.configx.ToList();
        }

        // GET api/<ConfigxController>/5
        [HttpGet("{id}")]
        public configx Get(int id)
        {
            return db.configx.Find(id);
        }

        // POST api/<ConfigxController>
        [HttpPost]
        public void Post([FromBody] configx value)
        {
            db.configx.Add(value);
            db.SaveChanges();
        }

        // PUT api/<ConfigxController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] configx value)
        {
            value.id = id;
            db.configx.Update(value);
            db.SaveChanges();
        }

        // DELETE api/<ConfigxController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            configx value = db.configx.Find(id);
            db.configx.Remove(value);
            db.SaveChanges();
        }
    }
}
