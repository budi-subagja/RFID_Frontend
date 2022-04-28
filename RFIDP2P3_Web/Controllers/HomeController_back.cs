using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
namespace RFIDP2P3_Web.Controllers
{
    public class HomeController_bac : Controller
    {
        public IActionResult Index(String username)
        {
            string str = HttpContext.Session.GetString("mysession");

            JArray array = JArray.Parse(str);
            JObject firstObject = (JObject)array.First;
            var userValue = firstObject.GetValue("PIC_ID");
            String user = (String)userValue;

            var adminValue = firstObject.GetValue("Privileges");
            var privilegesSerialize = JsonConvert.SerializeObject(adminValue);

            JArray arrays = JArray.Parse(privilegesSerialize);


            foreach (var item in arrays)
            {
                string id = (string)item["Menu_Id"];
                string read = (string)item["checkedbox_read"];
                Console.WriteLine(id + "_" + read);
            }

            //JObject firstObjectz = (JObject)arrays.First;

            //var password = firstObjectz.GetValue("Menu_Id");



            //ViewBag.name = user;
            //return base.Content("<div>" + password + "</div>", "text/html");
            return View();
        }
    }
}