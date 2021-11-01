using Jobweb.Filtros;
using Jobweb.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Jobweb.Controllers
{
    public class PosterController : Controller
    {
        string Baseurl = "https://jobwebapi.azurewebsites.net/"; //API Base URL
        // GET: Poster
        [Autorizaciones(nivel: "poster")]
        public async Task<ActionResult> PostAJob()
        {
            List<Categoria> ctgs = new List<Categoria>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/v1/Categoria");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CategoriaResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    ctgs = JsonConvert.DeserializeObject<List<Categoria>>(CategoriaResponse);

                }
            }
                ViewBag.categorias = ctgs;
                return View();
        }
        [HttpPost]
        [Autorizaciones(nivel: "poster")]
        public async Task<ActionResult> PostAJob(PuestoTrabajo puesto, Compañia company)
        {
           
            //confirmando que no vengan datos vacios
            if (company != null && puesto != null)
            {
                puesto.estado = true;
                //agregando puesto
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var userContent = JsonConvert.SerializeObject(puesto);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                    var byteContent = new ByteArrayContent(buffer);
                    //definiendo header
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource Usuario using HttpClient  
                    HttpResponseMessage Res = await client.PostAsync($"api/v1/PuestoTrabajo", byteContent);
                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {                        
                        ViewBag.mensaje = "Se ha agregado un nuevo puesto correctamente.";
                        return RedirectToAction("Index","Home",ViewBag.mensaje);
                    }

                }
            }
           
            return await PostAJob();
        }
        [Autorizaciones(nivel: "poster")]
        public ActionResult Confirm()
        {
            return View();
        }

        [HttpPost]
        [Autorizaciones(nivel: "poster")]
        public async Task<ActionResult> Confirm(PuestoTrabajo puesto, string categoria)
        {
            //guardando valores
            int.TryParse(categoria.Split('&')[1], out int idCategoria);
            puesto.idCategoria = idCategoria;
            ViewBag.categoria = categoria.Split('&')[0];
            ViewBag.tipo = puesto.tipo;
            ViewBag.idCategoria = puesto.idCategoria;
            ViewBag.ubicacion = puesto.ubicacion;
            ViewBag.aplicar = puesto.aplicar;
            ViewBag.posicion = puesto.posicion;
            Usuario user = (Usuario)Session["User"];
            if (user.tipo != "poster")
            {
                ViewBag.error = "Necesitas ser poster para agregar un puesto de trabajo.";
                return await PostAJob();
            }
            //estableciendo fecha y estado
            puesto.fechaPublicacion = DateTime.Now;
            puesto.estado = true;
            ViewBag.fechaPublicacion = puesto.fechaPublicacion;

            Compañia cp = null;
            //buscando la compañia del usuario actual
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync($"api/v1/Company/0?idUser={user.id}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CompanyResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    cp = JsonConvert.DeserializeObject<Compañia>(CompanyResponse);

                }
            }
            //agregando la compañia al puesto
            if (cp != null)
            {
                puesto.idCompañia = cp.id;
                ViewBag.puesto = puesto;
                ViewBag.company = cp;
                return View();
            }
            else
            {
                ViewBag.error = "Se ha producido un error al intentar obtener la empresa registrada con su usuario. Intente nuevamente.";
                return RedirectToAction("PostAJob","Poster",ViewBag);
            }
                
        }
    }
}