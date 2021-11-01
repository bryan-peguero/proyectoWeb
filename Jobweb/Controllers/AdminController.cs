using Jobweb.Filtros;
using Jobweb.Models;
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
    public class AdminController : Controller
    {
        string Baseurl = "https://jobwebapi.azurewebsites.net/"; //API Base URL
                                                     // GET: Admin
        [Autorizaciones(nivel: "administrador")]
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: Users
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> Users()
        {
            List<Usuario> Users = new List<Usuario>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/v1/Usuario");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var UserResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Users = JsonConvert.DeserializeObject<List<Usuario>>(UserResponse);

                }
            }
            return View(Users);
        }


        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditUser(int id)
        {
            Usuario user = new Usuario();
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync($"api/v1/Usuario/{id}");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var UserResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        user = JsonConvert.DeserializeObject<Usuario>(UserResponse);

                    }
                }
                return View(user);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditUser(Usuario user)
        {

            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var userContent = JsonConvert.SerializeObject(user);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.PutAsync($"api/v1/Usuario/{user.id}", byteContent);


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Users");
                    }
                }
                return View(user);
            }
            catch (Exception err)
            {
                return View(err);
            }
        }


        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> DeleteUser(int id)
        {

            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.DeleteAsync($"api/v1/Usuario/{id}");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Users");

                    }
                }
                return RedirectToAction("Users");
            }
            catch (Exception err)
            {
                return View(err);
            }
        }

        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> Categories()
        {
            List<Categoria> Categories = new List<Categoria>();

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
                    var CatResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Categories = JsonConvert.DeserializeObject<List<Categoria>>(CatResponse);

                }
            }
            return View(Categories);
        }
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditCategories(int id)
        {
            Categoria categoria = new Categoria();
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync($"api/v1/Categoria/{id}");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var CatResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        categoria = JsonConvert.DeserializeObject<Categoria>(CatResponse);

                    }
                }
                return View(categoria);
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpPost]
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditCategories(Categoria cat)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var userContent = JsonConvert.SerializeObject(cat);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.PutAsync($"api/v1/Categoria/{cat.id}", byteContent);


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Categories");
                    }
                }
                return View(cat);
            }
            catch (Exception err)
            {
                return View(err);
            }
        }
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> Listings()
        {
            List<PuestoTrabajo> Listings = new List<PuestoTrabajo>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/v1/PuestoTrabajo");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ListingResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Listings = JsonConvert.DeserializeObject<List<PuestoTrabajo>>(ListingResponse);

                }
            }
            return View(Listings);
        }
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditListing(int id)
        {
            PuestoTrabajo listing = new PuestoTrabajo();
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync($"api/v1/PuestoTrabajo/{id}");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ListingResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        listing = JsonConvert.DeserializeObject<PuestoTrabajo>(ListingResponse);

                    }
                }
                return View(listing);
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpPost]
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditListing(PuestoTrabajo listing)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var userContent = JsonConvert.SerializeObject(listing);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.PutAsync($"api/v1/PuestoTrabajo/{listing.id}", byteContent);


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Listings");
                    }
                }
                return View(listing);
            }
            catch (Exception err)
            {
                return View(err);
            }
        }
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> DeleteListing(int id)
        {

            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.DeleteAsync($"api/v1/PuestoTrabajo/{id}");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Listings");

                    }
                }
                return RedirectToAction("Listings");
            }
            catch (Exception err)
            {
                return View(err);
            }
        }
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> Settings()
        {
            List<Config> Configs = new List<Config>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/v1/Config");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ConfigResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Configs = JsonConvert.DeserializeObject<List<Config>>(ConfigResponse);

                }
            }
            return View(Configs);
        }
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditSettings(int id)
        {
            Config config = new Config();
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync($"api/v1/Config/{id}");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ConfigResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        config = JsonConvert.DeserializeObject<Config>(ConfigResponse);

                    }
                }
                return View(config);
            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpPost]
        [Autorizaciones(nivel: "administrador")]
        public async Task<ActionResult> EditSettings(Config config)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var userContent = JsonConvert.SerializeObject(config);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(userContent);
                    var byteContent = new ByteArrayContent(buffer);

                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.PutAsync($"api/v1/Config/{config.id}", byteContent);


                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Settings");
                    }
                }
                return View(config);
            }
            catch (Exception err)
            {
                return View(err);
            }
        }

    }
}