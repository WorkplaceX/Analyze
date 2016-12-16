using Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Server
{
    public class TodoController : Controller
    {
        // const string path = "/web/"; // Run with root debug page.
        const string path = "/"; // Run direct.

        [Route(path + "{*uri}")]
        public async Task<IActionResult> Web(Data data)
        {
            if (HttpContext.Request.Path == path)
            {
                bool isUniversal = true; // Use Angular Universal server side render engine.
                string result = null;
                if (isUniversal)
                {
                    string url = "http://" + Request.Host.ToUriComponent() + "/Universal/index.js";
                    using (HttpClient client = new HttpClient())
                    {
                        HttpResponseMessage response = await client.GetAsync(url);
                        if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        {
                            result = await UniversalExpress();
                        }
                        else
                        {
                            response.EnsureSuccessStatusCode();
                            result = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
                else
                {
                    result = System.IO.File.ReadAllText("Universal/index.html");
                }
                return Content(result, "text/html");
            }
            if (HttpContext.Request.Path == path + "api/data/")
            {
                var result = Application.Main.Request(data);
                return Json(result);
            }
            if (HttpContext.Request.Path == path + "Universal/index.js")
            {
                // Not running on IIS. Divert to UniversalExpress
                string url = "http://localhost:1337/";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                return Content(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Not running on IIS. Divert to UniversalExpress when running in Visual Studio.
        /// </summary>
        private async Task<string> UniversalExpress()
        {
            string url = "http://localhost:1337/";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                return result;
            }
        }
    }
}
