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
            return View("Application/Views/Home.cshtml");
        }
    }

    /// <summary>
    /// Install javascript files. Copy files on server to folder Application/Node.js/Client.
    /// </summary>
    public class AngularController : Controller
    {
        [Route("/Angular/{*url}")]
        public FileResult Angular()
        {
            // Thread.Sleep(100);
            return Util.FileGet(this, "Angular/", "../Angular/", "Application/Node.js/Client/"); // Copy requested files from Angular to wwwroot
        }
    }

    /// <summary>
    /// Angular2 Quickstart page.
    /// </summary>
    public class AngularInstallClient : Controller
    {
        [Route("/Angular/InstallClient.html")]
        public IActionResult Index()
        {
            return View("Application/Views/AngularInstallClient.cshtml");
        }
    }

    /// <summary>
    /// Angular2 transition from static to dynamic.
    /// </summary>
    public class AngularUniversalController : Controller
    {
        [Route("Angular/")]
        public IActionResult Index()
        {
            if (!Request.Path.Value.EndsWith("/"))
            {
                return Redirect(Request.Path.Value + "/"); // Script in served file need to reference to folder.
            }
            Data data = new Data() { Name = "Data from Controller.cs" };
            string dataJsonText = JsonConvert.SerializeObject(data);
            return View("Application/Views/AngularUniversal.cshtml", dataJsonText); // Pass data object from ASP.NET to Node.js
        }
    }

    /// <summary>
    /// Angular2 transition from static to dynamic with defer.
    /// </summary>
    public class AngularUniversalDeferController : Controller
    {
        [Route("Angular/Defer.html")]
        public IActionResult Index()
        {
            Data data = new Data() { Name = "Data from Controller.cs" };
            string dataJsonText = JsonConvert.SerializeObject(data);
            return View("Application/Views/AngularUniversalDefer.cshtml", dataJsonText); // Pass data object from ASP.NET to Node.js
        }

        [Route("Angular/defer.js")]
        public IActionResult Defer()
        {
            string result = System.IO.File.ReadAllText("Application/Node.js/defer.js");
            return Content(result);
        }
    }

    public class Data
    {
        public string Name { get; set; }
    }

    public static class Util
    {
        /// <summary>
        /// Uri for Windows and Linux.
        /// </summary>
        public class Uri
        {
            public Uri(string uriString)
            {
                if (uriString.StartsWith("/"))
                {
                    this.isLinux = true;
                    uriString = "Linux:" + uriString;
                }
                this.uriSystem = new System.Uri(uriString);
            }

            public Uri(Uri baseUri, string relativeUri)
            {
                this.uriSystem = new System.Uri(baseUri.uriSystem, relativeUri);
            }

            private readonly bool isLinux;

            private readonly System.Uri uriSystem;

            public string LocalPath
            {
                get
                {
                    string result = uriSystem.LocalPath;
                    if (isLinux)
                    {
                        result = result.Substring("Linux:".Length);
                    }
                    return result;
                }
            }
        }

        /// <summary>
        /// Copy file from source to dest and serve it.
        /// </summary>
        /// <param name="controller">WebApi controller</param>
        /// <param name="requestFolderName">For example: MyApp/</param>
        /// <param name="folderNameSourceRelative">For example ../Angular/</param>
        /// <param name="folderNameDestRelative">For example Application/Node.js/Client/</param>
        public static FileContentResult FileGet(ControllerBase controller, string requestFolderName, string folderNameSourceRelative, string folderNameDestRelative)
        {
            FileContentResult result = null;
            string requestFileName = controller.Request.Path.Value;
            string requestFolderNameMatch = requestFileName;
            if (!requestFolderNameMatch.EndsWith("/"))
            {
                requestFolderNameMatch += "/";
            }
            if (requestFolderNameMatch.StartsWith("/" + requestFolderName))
            {
                requestFileName = requestFileName.Substring(requestFolderName.Length + 1);
                Uri folderName = new Uri(Directory.GetCurrentDirectory() + "/");
                Uri folderNameSource = new Uri(folderName, folderNameSourceRelative);
                Uri folderNameDest = new Uri(folderName, folderNameDestRelative);
                Uri fileNameSource = new Uri(folderNameSource, requestFileName);
                Uri fileNameDest = new Uri(folderNameDest, requestFileName);
                // ContentType
                string fileNameExtension = Path.GetExtension(fileNameSource.LocalPath);
                string contentType; // https://wiki.selfhtml.org/wiki/Referenz:MIME-Typen
                switch (fileNameExtension)
                {
                    case ".html": contentType = "text/html"; break;
                    case ".css": contentType = "text/css"; break;
                    case ".js": contentType = "text/javascript"; break;
                    case ".map": contentType = "text/plain"; break;
                    default:
                        throw new Exception("Unknown!");
                }
                // Copye from source to dest
                if (File.Exists(fileNameSource.LocalPath) && !File.Exists(fileNameDest.LocalPath))
                {
                    string folderNameCopy = Directory.GetParent(fileNameDest.LocalPath).ToString();
                    if (!Directory.Exists(folderNameCopy))
                    {
                        Directory.CreateDirectory(folderNameCopy);
                    }
                    File.Copy(fileNameSource.LocalPath, fileNameDest.LocalPath);
                }
                // Serve dest
                var byteList = File.ReadAllBytes(fileNameDest.LocalPath);
                result = controller.File(byteList, contentType);
            }
            return result;
        }
    }
}
