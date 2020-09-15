using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantMVC.Models;

namespace RestaurantMVC.Controllers
{
    public class UsersController : Controller
    {
        Uri base_address = new Uri("https://localhost:44322/api");
        HttpClient client;
        public UsersController()
        {
            client = new HttpClient();
            client.BaseAddress = base_address;
        }
        public IActionResult New()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Users user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Users", content).Result;
            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { Email = user.Email });
            }
            return View();

        }
        public IActionResult Details(string Email)
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Users/"+Email).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Users user = JsonConvert.DeserializeObject<Users>(data);
                return View(user);
            }
            return View();

        }
        public IActionResult Edit(string Email)
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Users/" + Email).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                Users user = JsonConvert.DeserializeObject<Users>(data);
                return View(user);
            }
            return View();
        }
        public IActionResult Update(Users user)
        {

            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Users/"+user.Email, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { Email = user.Email });
            }
            return View();

        }
    }
}
