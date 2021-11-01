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
using System.IO;
using System.Web.UI.WebControls;
using System.Xml.Schema;
using System.Dynamic;

namespace Jobweb.Controllers
{
    public class HomeController : Controller
    {
        string Baseurl = "https://jobwebapi.azurewebsites.net/"; //API Base URL
        static int listingCount = 0;
        static int categoryCount = 0;

        public async Task<ActionResult> Index()
        {
            dynamic model = new ExpandoObject();
            List<Listing> Puesto = new List<Listing>();
            List<Categoria> Categories = new List<Categoria>();
            Config puestosMaximos = new Config();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/v1/Listing");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var PuestoResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Puesto = JsonConvert.DeserializeObject<List<Listing>>(PuestoResponse);
                    Puesto = Puesto.OrderBy(d => d.fechaPublicacion).ToList();
                    listingCount = Puesto.Count();
                    model.Puesto = Puesto;
                }

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                Res = await client.GetAsync("api/v1/Categoria");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CatResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Categories = JsonConvert.DeserializeObject<List<Categoria>>(CatResponse);
                    Categories = Categories.Where(c => c.disponibilidad == 1).ToList();
                    categoryCount = Categories.Count();
                    model.Categories = Categories;
                }

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                Res = await client.GetAsync("api/v1/Config/6");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CatResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    puestosMaximos = JsonConvert.DeserializeObject<Config>(CatResponse);
                    model.Config = puestosMaximos;
                }
            }
            return View(model);
        }

        public ActionResult Log()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Log(string user, string pass)
        {
            ViewBag.username = user;
            Usuario usr = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource Usuario using HttpClient  
                HttpResponseMessage Res = await client.GetAsync($"api/v1/Usuario/0?user={user}");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UsuarioResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    usr = JsonConvert.DeserializeObject<Usuario>(UsuarioResponse);
                }

            }
            if (usr != null) 
            {
                if (pass == usr.password)
                {
                    Session["User"] = usr;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Contraseña Incorrecta";
                }                
            }               
            else
            {
                ViewBag.Error = "El usuario no existe";
            }
            return View();



        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Log", "Home");            
        }

        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Signup(Usuario user, string password2, Compañia company = null, HttpPostedFileBase logoImg = null)
        {

            ViewBag.user = user.username;
            ViewBag.tipo = user.tipo;             
            ViewBag.nombre = company.nombre;
            ViewBag.correo = company.email;
            ViewBag.url = company.url; 

            if(user != null && password2 != null && (user.password != password2))
            {
                ViewBag.Error = "Las contraseñas no coincieden";
                return View();
            }
            if(logoImg != null && !logoImg.FileName.EndsWith(".jpg") && !logoImg.FileName.EndsWith(".png"))
            {
                ViewBag.Error = "Solo se aceptan imagenes de tipo .JPG o .PNG";
                return View();
            }
            user.tipo = user.tipo.ToLower();
            Usuario usr = null;
            //verificando que el usuario no existe
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource Usuario using HttpClient  
                HttpResponseMessage Res = await client.GetAsync($"api/v1/Usuario/0?user={user.username}");
                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UsuarioResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    usr = JsonConvert.DeserializeObject<Usuario>(UsuarioResponse);
                }

               
            }
            if (usr != null)
            {
                ViewBag.Error = "Este nombre de usuario ya existe, elije otro";
                return View();
            }
            else
            {           
                //Creando usuario
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var userContent = JsonConvert.SerializeObject(user);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                    var byteContent = new ByteArrayContent(buffer);
                    //definiendo header
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource Usuario using HttpClient  
                    HttpResponseMessage Res = await client.PostAsync($"api/v1/Usuario", byteContent);
                    //Checking the response is successful or not which is sent using HttpClient  

                }
                if(user.tipo == "poster" && company != null)
                {                
                    //obteniendo id del usuario creado
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        client.DefaultRequestHeaders.Clear();
                        //Define request data format  
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        //Sending request to find web api REST service resource Usuario using HttpClient  
                        HttpResponseMessage Res = await client.GetAsync($"api/v1/Usuario/0?user={user.username}");
                        //Checking the response is successful or not which is sent using HttpClient  
                        if (Res.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api   
                            var UsuarioResponse = Res.Content.ReadAsStringAsync().Result;

                            //Deserializing the response recieved from web api and storing into the Employee list  
                            usr = JsonConvert.DeserializeObject<Usuario>(UsuarioResponse);
                        }
                    }
                    //comprobando la respuesta
                    if(usr != null)
                    {               
                        //pasando id al objeto comapany
                        company.idUsuario = usr.id;
                        //creando ruta donde se guardara el logo                 
                        if (logoImg != null)
                        {
                            company.logo = $"logo-{company.nombre}-{company.idUsuario}.jpg";
                        }
                        //Creando company
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri(Baseurl);
                            client.DefaultRequestHeaders.Clear();
                            //Define request data format  
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            //Convirtiendo a json
                            var userContent = JsonConvert.SerializeObject(company);
                            var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                            var byteContent = new ByteArrayContent(buffer);
                            //definiendo header
                            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                            //Sending request to find web api REST service resource Usuario using HttpClient  
                            HttpResponseMessage Res = await client.PostAsync($"api/v1/Company", byteContent);
                            //Checking the response is successful or not which is sent using HttpClient  
                            if (Res.IsSuccessStatusCode)
                            {
                                if(logoImg != null)
                                {
                                    string ruta = Server.MapPath("~");
                                    ruta += "\\Assets\\" + company.logo;
                                    logoImg.SaveAs(ruta);                               
                                }
                            }
                        }

                    }
                }

                return RedirectToAction("Log", "Home");
            }                            
        }

        public ActionResult Error()
        {
            return View();
        }

        public static int GetListingCount()
        {
            return listingCount;
        }

        public static int GetCategoryCount()
        {
            return categoryCount;
        }
    }
}