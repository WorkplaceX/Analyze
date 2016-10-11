using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    public class JavaScriptController : Controller
    {
        [Route("/JavaScript")]
        public IActionResult Index()
        {
            Data data = new Data() { Name = "Data from Controller.cs" };
            string dataJsonText = JsonConvert.SerializeObject(data);
            return View("JavaScript.cshtml", dataJsonText); // Pass data object from ASP.NET to Node.js
        }
    }

    public class Data
    {
        public string Name { get; set; }
    }
}
