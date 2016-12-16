using Application;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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
            data = Main.Request(data);
            // Html
            if (HttpContext.Request.Path == path)
            {
                bool isUniversal = true; // Use Angular Universal server side render engine.
                string result = null;
                if (isUniversal)
                {
                    string url = "http://" + Request.Host.ToUriComponent() + "/Universal/index.js";
                    result = await Post(url, data, false); // Call Angular Universal server side rendering service.
                    if (result == null)
                    {
                        url = "http://localhost:1337/"; // Application not running on IIS. Divert to UniversalExpress when running in Visual Studio.
                        result = await Post(url, data, true);
                    }
                }
                else
                {
                    result = System.IO.File.ReadAllText("Universal/index.html");
                }
                return Content(result, "text/html");
            }
            // Data API
            if (HttpContext.Request.Path == path + "api/data/")
            {
                var result = Application.Main.Request(data);
                return Json(result);
            }
            return NotFound();
        }

        /// <summary>
        /// Post json data to url.
        /// </summary>
        private async Task<string> Post(string url, Data data, bool isEnsureSuccessStatusCode)
        {
            string json = JsonConvert.SerializeObject(data);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(url, new StringContent(json, Encoding.Unicode, "application/json"));
                if (isEnsureSuccessStatusCode)
                {
                    response.EnsureSuccessStatusCode();
                    string result = await response.Content.ReadAsStringAsync();
                    return result;
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
    }
}
