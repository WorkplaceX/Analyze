using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication
{
    public class HomeController : Controller
    {
        [Route("/Home")]
        public IActionResult Index()
        {
            return View("Home.cshtml");
        }
    }

    /// <summary>
    /// Load javascript files. And copy them to wwwroot.
    /// </summary>
    public class AngularController : Controller
    {
        [Route("/Angular/{*url}")]
        public FileResult Angular()
        {
            // Thread.Sleep(100);
            return Util.FileGet(this, "Angular/"); // Copy requested files from Angular to wwwroot
        }
    }

    /// <summary>
    /// Angular2 Quickstart page.
    /// </summary>
    public class AngularDebugController : Controller
    {
        [Route("/Angular/Debug.html")]
        public IActionResult Index()
        {
            return View("AngularDebug.cshtml");
        }
    }

    /// <summary>
    /// Angular2 transition from static to dynamic.
    /// </summary>
    public class AngularUniversalController : Controller
    {
        [Route("/Angular")]
        public IActionResult Index()
        {
            Data data = new Data() { Name = "Data from Controller.cs" };
            string dataJsonText = JsonConvert.SerializeObject(data);
            return View("AngularUniversal.cshtml", dataJsonText); // Pass data object from ASP.NET to Node.js
        }
    }


    public class Data
    {
        public string Name { get; set; }
    }

    public static class Util
    {
        public static FileResult FileGet(ControllerBase controller, string folderNameRelative)
        {
            string requestFileName = controller.Request.Path.Value;
            if (requestFileName.StartsWith("/" + folderNameRelative))
            {
                requestFileName = requestFileName.Substring(("/" + folderNameRelative).Length);
                Uri folderName = new Uri(Directory.GetCurrentDirectory() + @"\");
                Uri folderNameAngular = new Uri(folderName, "../../" + folderNameRelative);
                Uri fileNameAngular = new Uri(folderNameAngular, requestFileName);
                Uri folderNameRoot = new Uri(folderName, "wwwroot/" + folderNameRelative);
                Uri fileNameRoot = new Uri(folderNameRoot, requestFileName);
                if (File.Exists(fileNameRoot.LocalPath) || System.IO.File.Exists(fileNameAngular.LocalPath))
                {
                    if (!File.Exists(fileNameRoot.LocalPath))
                    {
                        string folderNameCopy = Directory.GetParent(fileNameRoot.LocalPath).ToString();
                        if (!Directory.Exists(folderNameCopy))
                        {
                            Directory.CreateDirectory(folderNameCopy);
                        }
                        File.Copy(fileNameAngular.LocalPath, fileNameRoot.LocalPath);
                    }
                    var byteList = File.ReadAllBytes(fileNameRoot.LocalPath);
                    string fileNameExtension = Path.GetExtension(fileNameAngular.LocalPath);
                    string contentType;
                    switch (fileNameExtension)
                    {
                        case ".html": contentType = "text/html"; break;
                        case ".css": contentType = "text/css"; break;
                        case ".js": contentType = "text/javascript"; break;
                        case ".map": contentType = "text/plain"; break;
                        default:
                            throw new Exception("Unknown!");
                    }
                    return controller.File(byteList, contentType); // https://wiki.selfhtml.org/wiki/Referenz:MIME-Typen
                }
            }
            return null;
        }
    }
}
