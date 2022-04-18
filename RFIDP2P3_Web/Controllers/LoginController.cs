using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RFIDP2P3_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace RFIDP2P3_Web.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult AddLogin() => View();

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);
            string apiResponse;

            using (client)
            {
                User userLogin = new User();
                userLogin.PIC_ID = username;
                userLogin.password = password;
                StringContent content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");

                using (var response = await client.PostAsync("https://localhost:7072/api/Login", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "User not found/not active")
                    {
                        ViewBag.Message = "User not found/not active";
                        return View();
                    }
                    else if (apiResponse == "Incorrect login/password")
                    {
                        ViewBag.IncorrectMessage = "Incorrect login/password";
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { username = username });
                        //userLogin = JsonConvert.DeserializeObject<User>(apiResponse);
                        //return RedirectToAction("Index", "Home");
                    }
                }
            }
        }
    }
}
