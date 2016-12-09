using Application;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Server
{
    public class TodoController : Controller
    {
        [Route("web01/")]
        public string Web01()
        {
            string result = System.IO.File.ReadAllText("Render/src/index.html");
            Response.ContentType = "text/html";
            return result;
        }

        [Route("web01/api/data/")]
        public Data Data(Data data)
        {
            var result = Application.Main.Request(data);
            return result;
        }
    }
}
