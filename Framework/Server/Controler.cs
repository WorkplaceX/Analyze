namespace Server
{
    using Framework.Server.Application;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class WebController : Controller
    {
        // const string path = "/web/"; // Run with root debug page.
        const string path = "/"; // Run direct.

        [Route(path + "{*uri}")]
        public async Task<IActionResult> Web()
        {
            // Html
            if (HttpContext.Request.Path == path)
            {
                Data dataOut = new Application.ApplicationX().Process(null);
                string htmlUniversal = null;
                string html = IndexHtml(true);
                htmlUniversal = await HtmlUniversal(html, dataOut, true); // Angular Universal server side rendering.
                return Content(htmlUniversal, "text/html");
            }
            // Data API
            if (HttpContext.Request.Path == path + "data.json")
            {
                string jsonIn = Util.StreamToString(Request.Body);
                Data dataIn = Framework.Server.Json.Util.Deserialize<Data>(jsonIn);
                Data dataOut = new Application.ApplicationX().Process(dataIn);
                string jsonOut = Framework.Server.Json.Util.Serialize(dataOut);
                return Content(jsonOut, "application/json");
            }
            // node_modules
            if (HttpContext.Request.Path.ToString().StartsWith("/node_modules/"))
            {
                return Util.FileGet(this, "", "../Client/", "Universal/");
            }
            // (*.css; *.js)
            if (HttpContext.Request.Path.ToString().EndsWith(".css") || HttpContext.Request.Path.ToString().EndsWith(".js"))
            {
                return Util.FileGet(this, "", "Universal/", "Universal/");
            }
            return NotFound();
        }

        /// <summary>
        /// Returns server side rendered index.html.
        /// </summary>
        private async Task<string> HtmlUniversal(string html, Data data, bool isUniversal)
        {
            if (isUniversal == false)
            {
                return html;
            }
            else
            {
                string htmlUniversal = null;
                string url = "http://" + Request.Host.ToUriComponent() + "/Universal/index.js";
                data.IsBrowser = false; // Server side rendering mode.
                string json = Framework.Server.Json.Util.Serialize(data);
                htmlUniversal = await Post(url, json, false); // Call Angular Universal server side rendering service.
                if (htmlUniversal == null)
                {
                    url = "http://localhost:1337/"; // Application not running on IIS. Divert to UniversalExpress when running in Visual Studio.
                    htmlUniversal = await Post(url, json, true);
                    Util.Assert(htmlUniversal != "<app></app>"); // Catch java script errors. See UniversalExpress console for errors!
                }
                //
                string result = null;
                // Replace <app> on index.html
                {
                    int indexBegin = htmlUniversal.IndexOf("<app>");
                    int indexEnd = htmlUniversal.IndexOf("</app>") + "</app>".Length;
                    string htmlUniversalClean = htmlUniversal.Substring(indexBegin, (indexEnd - indexBegin));
                    result = html.Replace("<app>Loading AppComponent content here ...</app>", htmlUniversalClean);
                }
                data.IsBrowser = true; // Client side rendering mode.
                string dataJson = Framework.Server.Json.Util.Serialize(data);
                string resultAssert = result;
                // Add data to index.html (Client/index.html)
                {
                    string scriptFind = "System.import('app').catch(function(err){ console.error(err); });";
                    string scriptReplace = "var browserData = '" + dataJson + "'; " + scriptFind;
                    result = result.Replace(scriptFind, scriptReplace);
                }
                // Add data to index.html (Server/indexBundle.html)
                {
                    string scriptFind = "function downloadJSAtOnload() {";
                    string scriptReplace = "var browserData = '" + dataJson + "';\r\n" + scriptFind;
                    result = result.Replace(scriptFind, scriptReplace);
                }
                Util.Assert(resultAssert != result, "Adding browserData failed!");
                return result;
            }
        }

        /// <summary>
        /// Post json data to url.
        /// </summary>
        private async Task<string> Post(string url, string json, bool isEnsureSuccessStatusCode)
        {
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

        /// <summary>
        /// Returns index.html.
        /// </summary>
        /// <param name="isBundle">If true use Server/index.html else Client/index.html</param>
        private string IndexHtml(bool isBundle)
        {
            if (isBundle == false)
            {
                return System.IO.File.ReadAllText("Universal/index.html"); // Original source: Client/index.html
            }
            else
            {
                return System.IO.File.ReadAllText("indexBundle.html"); // Original source: Client/index.html
            }
        }
    }
}
