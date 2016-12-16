using Application;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Server
{
    public class TodoController : Controller
    {
        [Route("/web01/{*uri}")]
        public IActionResult Web01(Data data)
        {
            if (HttpContext.Request.Path == "/web01/")
            {
                string result = System.IO.File.ReadAllText("Universal/index.html");
                return Content(result, "text/html");
            }
            if (HttpContext.Request.Path == "/web01/api/data/")
            {
                var result = Application.Main.Request(data);
                return Json(result);
            }
            return NotFound();
        }
    }
}
