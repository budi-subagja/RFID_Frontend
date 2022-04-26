using RFIDP2P3_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;

namespace RFIDP2P3_Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("PIC_ID") == null) return View();
            else return RedirectToAction("Index", "Home");
        }

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

                using (var response = await client.PostAsync("https://localhost:7072/api/Login/Index", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse == "User not found/not active")
                    {
                        ViewBag.Message = "User not found/not active";
                        return View();
                    }
                    else if (apiResponse == "Incorrect login/password")
                    {
                        ViewBag.Message = "Incorrect login/password";
                        return View();
                    }
                    else
                    {
<<<<<<< HEAD
                        
                        //ambil res di parse jadi array
                        JArray array = JArray.Parse(apiResponse);
                        //diserialize untuk masuk ke session
                        var sessionSerialize = JsonConvert.SerializeObject(array);
                        //masok session
                        HttpContext.Session.SetString("mysession", sessionSerialize);


                        /*
                        JObject firstObject = (JObject)array.First;
                        var adminValue = firstObject.GetValue("Privileges");
                        string str = HttpContext.Session.GetString("mysession");
                        JArray arrays = JArray.Parse(str);
                        //JObject firstObjectz = (JObject)arrays.First;
                        */
                        return RedirectToAction("Index", "Home");
                        
                        //buat log
                        //return base.Content("<div>" + firstObjectz + "</div>", "text/html");
=======
                        userLogin = JsonConvert.DeserializeObject<User>(apiResponse.Substring(1, apiResponse.Length - 2));
                        HttpContext.Session.SetString("PIC_ID", userLogin.PIC_ID);
                        HttpContext.Session.SetString("password", userLogin.password);
                        HttpContext.Session.SetString("PIC_Name", userLogin.PIC_Name);
                        HttpContext.Session.SetString("UserGroup_Id", userLogin.UserGroup_Id);
                        foreach (var privilege in userLogin.Privileges)
                        {
                            HttpContext.Session.SetString("read_" + privilege.Menu_Id, privilege.checkedbox_read);
                            HttpContext.Session.SetString("add_" + privilege.Menu_Id, privilege.checkedbox_add);
                            HttpContext.Session.SetString("edit_" + privilege.Menu_Id, privilege.checkedbox_edit);
                            HttpContext.Session.SetString("del_" + privilege.Menu_Id, privilege.checkedbox_del);
                        }
                        return RedirectToAction("Index", "Home");
>>>>>>> 0ce7495fe995d9d502772863d90ddeb2410d079b
                    }
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            // Pass the handler to httpclient(from you are calling api)
            HttpClient client = new HttpClient(clientHandler);
            string apiResponse;

            using (client)
            {
                User userLogin = new User();
                userLogin.PIC_ID = "admin";
                StringContent content = new StringContent(JsonConvert.SerializeObject(userLogin), Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");

                using (var response = await client.PostAsync("https://localhost:7072/api/Login/Logout", content))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                    if (apiResponse != "success") return RedirectToAction("Index", "Home");
                    else return RedirectToAction("Index", "Login");
                }
            }
        }
    }
}
