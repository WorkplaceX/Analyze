using Microsoft.AspNetCore.Hosting;

namespace aspnetcoreapp
{
    using Microsoft.EntityFrameworkCore;
    using MySQL.Data.EntityFrameworkCore.Extensions;

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;userid=root;pwd=*********;database=My;sslmode=none;");
        }

        public DbSet<Person> Person { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}