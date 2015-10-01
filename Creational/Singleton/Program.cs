using static System.Console;
using System;
using System.Collections.Generic;

namespace Singleton
{
    public class Program
    {
        /// <summary>
        /// Singleton Design Pattern
        /// </summary>
        public static void Main()
        {
            var b1 = LoadBalancer.GetLoadBalancer();
            var b2 = LoadBalancer.GetLoadBalancer();
            var b3 = LoadBalancer.GetLoadBalancer();
            var b4 = LoadBalancer.GetLoadBalancer();

            // Confirm these are the same instance
            if (b1 == b2 && b2 == b3 && b3 == b4)
            {
                WriteLine("Same instance\n");
            }

            var balancer = LoadBalancer.GetLoadBalancer();

            // Load balance 15 requests for a server
            for (int i = 0; i < 15; i++)
            {
                var server = balancer.NextServer.Name;
                WriteLine("Dispatch request to: " + server);
            }

            // Wait for user
            ReadKey();
        }
    }

    /// <summary>
    /// The 'Singleton' class
    /// </summary>
    public sealed class LoadBalancer
    {
        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        private static readonly LoadBalancer instance = new();

        private readonly List<Server> servers;
        private readonly Random random = new();

        // Note: constructor is 'private'
        private LoadBalancer()
        {
            // Load list of available servers

            servers = [
                        new(Name: "ServerI", Ip: "120.14.220.18"),
                        new(Name: "ServerII", Ip: "120.14.220.19" ),
                        new(Name: "ServerIII", Ip: "120.14.220.20" ),
                        new(Name: "ServerIV", Ip: "120.14.220.21" ),
                        new(Name: "ServerV", Ip: "120.14.220.22" )
                      ];
        }

        public static LoadBalancer GetLoadBalancer()
        {
            return instance;
        }

        // Simple, but effective load balancer
        public Server NextServer
        {
            get => servers[random.Next(servers.Count)];
        }
    }

    /// <summary>
    /// Represents a server machine
    /// </summary>
    public record Server(string Name, string Ip);
}