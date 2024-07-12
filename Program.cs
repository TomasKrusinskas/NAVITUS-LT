using System;
using Nancy.Hosting.Self;

namespace ClientManagementAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("http://localhost:5000");
            var hostConfig = new HostConfiguration { UrlReservations = new UrlReservations { CreateAutomatically = true } };
            using (var host = new NancyHost(hostConfig, uri))
            {
                host.Start();
                Console.WriteLine("Client Management API is running on " + uri);
                Console.ReadLine();
            }
        }
    }
}
