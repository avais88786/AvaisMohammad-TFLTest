using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using TFLTest_AvaisMohammad.Models;
using TFLTest_AvaisMohammad.Services;

namespace TFLTest_AvaisMohammad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var container = Initialise();

            var roadStatusService = container.Resolve<IStatusService>(Constants.ServiceType.ROAD.ToString());

            var roadName = (args != null && args.Length > 0) ? args[0] : string.Empty;

            if (string.IsNullOrEmpty(roadName))
            {
                Console.WriteLine("What road would you like to check status of?");
                roadName = Console.ReadLine();
            }

            var loaded = false;
            Console.WriteLine("Please wait...");
            roadStatusService.GetStatus(roadName).ContinueWith(res =>
            {
                loaded = true;
                if (!res.IsFaulted && res.Result.Found)
                {
                    res.Result.GetStatusInfo();
                    Environment.Exit(0);
                }
                
                if (!res.IsFaulted && !res.Result.Found)
                {
                    Console.WriteLine(res.Result.FriendlyMessage);
                    Environment.Exit(1);
                }

                if (res.Exception.InnerExceptions.Any(x => x.GetType() == typeof(TflException)))
                {
                    Console.WriteLine(string.Join("\n", 
                                        res.Exception.InnerExceptions.Where(x => x.GetType() == typeof(TflException))?.Select(y => ((TflException)y).Message)));
                }
                else
                {
                    Console.WriteLine("Unexpected error encountered while identifying the road status.\nPlease check configuration settings and try again.");
                }
                Environment.Exit(1);
            });

            do { Console.ReadLine(); } while (!loaded);
        }

        private static WindsorContainer Initialise()
        {
            var container = new WindsorContainer();

            IConfigurationRoot configuration = GetConfigurationSettings(Directory.GetCurrentDirectory());

            if (string.IsNullOrEmpty(configuration["apiEndpointBase"]) || string.IsNullOrEmpty(configuration["appId"]) || string.IsNullOrEmpty(configuration["appKey"]))
            {
                throw new Exception($"Configuration settings are invalid. Please configure appsettings.<environment>.json in {Directory.GetCurrentDirectory()}");
            }

            container.Register(Castle.MicroKernel.Registration.Component.For<IStatusService>().ImplementedBy<RoadStatusService>().Named(Constants.ServiceType.ROAD.ToString()));
            container.Register(Castle.MicroKernel.Registration.Component.For<IConfiguration>().LifestyleSingleton().Instance(configuration));
            container.Register(Castle.MicroKernel.Registration.Component.For<HttpClient>().LifestyleSingleton().Instance(new HttpClient()));

            return container;
        }

        public static IConfigurationRoot GetConfigurationSettings(string basePath)
        {
            var environment = Environment.GetEnvironmentVariable("Environment") ?? "development";

            var builder = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);

            return builder.Build();

        }

    }
}
