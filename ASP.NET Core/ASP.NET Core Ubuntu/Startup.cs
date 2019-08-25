using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace aspnetcoreapp
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                try
                {
                    StringBuilder text = new StringBuilder();
                    var myContext = new MyContext();
                    foreach (var item in myContext.Person)
                    {
                        text.AppendLine($"Id=" + item.Id + "; " + "Name=" + item.Name + "; ");
                    }
                    //
                    return context.Response.WriteAsync("Hello from ASP.NET Core!\r\r" + text.ToString());
                }
                catch (Exception exception)
                {
                    return context.Response.WriteAsync("Error! (" + exception.Message + ")");
                }
            });
        }
    }
}