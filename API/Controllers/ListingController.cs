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
    public class ListingController : ControllerBase
    {
        private JobWebDB db = new JobWebDB();
        // GET: api/Listing or GET: api/Listing?search=query
        [HttpGet]
        public IEnumerable<Listing> Get(string search = "")
        {

            if (search == "")
            {

                List<Listing> all = (from job in db.PuestoTrabajo
                                     join com in db.Compañia on job.idCompañia equals com.id
                                     join cat in db.Categoria on job.idCategoria equals cat.id
                                     select new
                                     {
                                         Id = job.id,
                                         Nombre = com.nombre,
                                         Categoria = cat.categoria,
                                         Tipo = job.tipo,
                                         Posicion = job.posicion,
                                         Ubicacion = job.ubicacion,
                                         Logo = com.logo,
                                         Estado = job.estado,
                                         Fecha = job.fechaPublicacion,
                                         Descripcion = job.descripcion,
                                         Aplicar = job.aplicar
                                     }).Where(q => q.Estado == true).AsEnumerable().Select(x => new Listing
                                     {
                                         id = x.Id,
                                         company = x.Nombre,
                                         categoria = x.Categoria,
                                         tipo = x.Tipo,
                                         posicion = x.Posicion,
                                         ubicacion = x.Ubicacion,
                                         logo = x.Logo,
                                         fechaPublicacion = x.Fecha,
                                         descripcion = x.Descripcion,
                                         aplicar = x.Aplicar
                                     }).ToList();

                return all;
            }

            List<Listing> q = db.PuestoTrabajo.Join(db.Compañia,
                jobs => jobs.idCompañia,
                com => com.id,
                (jobs, com) => new { Jobs = jobs, Com = com }).Join(db.Categoria,
                jobs => jobs.Jobs.idCategoria,
                cat => cat.id,
                (jobs, cat) => new { Jobs = jobs, Cat = cat }).Where(
                q => q.Jobs.Jobs.estado == true &&
                (q.Jobs.Jobs.posicion.Contains(search) ||
                q.Jobs.Jobs.ubicacion.Contains(search) ||
                q.Cat.categoria.Contains(search) ||
                q.Jobs.Com.nombre.Contains(search))).Select(q => new
                {
                    Id = q.Jobs.Jobs.id,
                    Nombre = q.Jobs.Com.nombre,
                    Categoria = q.Cat.categoria,
                    Tipo = q.Jobs.Jobs.tipo,
                    Posicion = q.Jobs.Jobs.posicion,
                    Ubicacion = q.Jobs.Jobs.ubicacion,
                    Logo = q.Jobs.Com.logo,
                    Fecha = q.Jobs.Jobs.fechaPublicacion,
                    Descripcion = q.Jobs.Jobs.descripcion,
                    Aplicar = q.Jobs.Jobs.aplicar
                }).AsEnumerable().Select(x => new Listing
                {
                    id = x.Id,
                    company = x.Nombre,
                    categoria = x.Categoria,
                    logo = x.Logo,
                    posicion = x.Posicion,
                    tipo = x.Tipo,
                    ubicacion = x.Ubicacion,
                    fechaPublicacion = x.Fecha,
                    descripcion = x.Descripcion,
                    aplicar = x.Aplicar
                }).ToList();

            return q;
        }

        // GET: api/Listing/5
        [HttpGet("{id}")]
        public Listing Get(int id)
        {
            List<Listing> q = db.PuestoTrabajo.Join(db.Compañia,
                jobs => jobs.idCompañia,
                com => com.id,
                (jobs, com) => new { Jobs = jobs, Com = com }).Join(db.Categoria,
                jobs => jobs.Jobs.idCategoria,
                cat => cat.id,
                (jobs, cat) => new { Jobs = jobs, Cat = cat }).Where(q => q.Jobs.Jobs.id == id).Select(q => new
                {
                    Id = q.Jobs.Jobs.id,
                    Nombre = q.Jobs.Com.nombre,
                    Categoria = q.Cat.categoria,
                    Tipo = q.Jobs.Jobs.tipo,
                    Posicion = q.Jobs.Jobs.posicion,
                    Ubicacion = q.Jobs.Jobs.ubicacion,
                    Logo = q.Jobs.Com.logo,
                    Fecha = q.Jobs.Jobs.fechaPublicacion,
                    Descripcion = q.Jobs.Jobs.descripcion,
                    Aplicar = q.Jobs.Jobs.aplicar,
                    Email = q.Jobs.Com.email,
                    Url = q.Jobs.Com.url
                }).AsEnumerable().Select(x => new Listing
                {
                    id = x.Id,
                    company = x.Nombre,
                    categoria = x.Categoria,
                    logo = x.Logo,
                    posicion = x.Posicion,
                    tipo = x.Tipo,
                    ubicacion = x.Ubicacion,
                    fechaPublicacion = x.Fecha,
                    descripcion = x.Descripcion,
                    aplicar = x.Aplicar,
                    email = x.Email,
                    url = x.Url
                }).ToList();

            return q[0];
        }
    }
}
