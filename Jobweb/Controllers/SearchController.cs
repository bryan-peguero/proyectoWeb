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
    public class SearchController : Controller
    {
        string Baseurl = "https://jobwebapi.azurewebsites.net/"; //API Base URL

        // GET: Search
        public async Task<ActionResult> Index(string search = "", int page = 1)
        {
            List<Listing> Listings = new List<Listing>();
            var searchView = new Search();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res;
                if(search == "")
                {
                    Res = await client.GetAsync("api/v1/Listing");
                }
                else
                {
                    Res = await client.GetAsync($"api/v1/Listing?search={search}");

                }

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ListingResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    Listings = JsonConvert.DeserializeObject<List<Listing>>(ListingResponse);
                    Listings.OrderBy(d => d.fechaPublicacion).ToList();

                    searchView = new Search
                    {
                        ListingPerPage = 20,
                        Listings = Listings,
                        CurrentPage = page
                    };

                }
            }
            return View(searchView);
        }
    }
}