namespace WebApplication
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using System.IO;

    public class HomeController : Controller
    {
        [Route("/Home")]
        public IActionResult Index()
        {
            return View("Home.cshtml");
        }
    }

    public class JavaScriptController : Controller
    {
        [Route("/JavaScript")]
        public IActionResult Index()
        {
            return View("JavaScript.cshtml");
        }
    }

    public class AngularController : Controller
    {
        [Route("Angular/{*url}")]
        public FileResult Angular()
        {
            return Util.FileGet(this, "Angular/"); // Copy requested files from Angular to wwwroot
        }
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
