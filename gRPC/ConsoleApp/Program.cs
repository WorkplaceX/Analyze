using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcService;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // HTTP/2
            {
                using var channel = GrpcChannel.ForAddress("https://localhost:5001");
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
                Console.WriteLine("Greeting: " + reply.Message);
            }

            // HTTP/1.x
            {
                var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());
                var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpHandler = httpHandler });
                var client = new Greeter.GreeterClient(channel);
                var reply = await client.SayHelloAsync(new HelloRequest { Name = "GreeterClient" });
                Console.WriteLine("Greeting: " + reply.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
