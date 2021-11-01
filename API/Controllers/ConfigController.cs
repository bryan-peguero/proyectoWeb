using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private JobWebDB db = new JobWebDB();
        // GET: api/Config
        [HttpGet]
        public IEnumerable<Config> Get()
        {

            return db.Config.ToList();
        }

        // GET: api/Config/5
        [HttpGet("{id}")]
        public Config Get(int id)
        {
            return db.Config.Find(id);
        }

        // POST: api/Config
        [HttpPost]
        public void Post([FromBody] Config value)
        {
            db.Config.Add(value);
            db.SaveChanges();
        }

        // PUT: api/Config/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Config value)
        {
            value.id = id;
            db.Config.Update(value);
            db.SaveChanges();
        }

        // DELETE: api/Config/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Config value = db.Config.Find(id);
            db.Config.Remove(value);
            db.SaveChanges();
        }
    }
}
