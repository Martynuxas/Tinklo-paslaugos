using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using NLog;


namespace Server
{
	/// <summary>
	/// Application entry point.
	/// </summary>
	public class Server
	{
		public static double tank { get; set; } = 0;
		public static double reputation { get; set; } = 0;
		public static double fillingLevel { get; set; } = 0;
		public static bool queueIsFull { get; set; } = false;
		/// <summary>
        /// Logger for this class.
        /// </summary>
        Logger log = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Configure loggin subsystem.
        /// </summary>
        private void ConfigureLogging() 
        {
            var config = new NLog.Config.LoggingConfiguration();

            var console =
                new NLog.Targets.ConsoleTarget("console") 
                {
                    Layout = @"${date:format=HH\:mm\:ss}|${level}| ${message} ${exception}"
                };
            config.AddTarget(console);            
            config.AddRuleForAllLevels(console);

            LogManager.Configuration = config;
        }
		public void CreateTank(){
			var random = new Random();
			tank = random.Next(100,1000);
		}
		/// <summary>
		/// Program body.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		void Run(string[] args)
		{
			CreateTank();
			//configure logging
            ConfigureLogging();


			//configure and start the server
			CreateHostBuilder(args).Build().RunAsync();

			//suspend the main thread
			
			log.Info("Server has started.");
			log.Info($"Gas station has {tank}l of fuel");
			log.Info($"Gas station has {reputation} reputation");
			log.Info("Gas station queue is ");
			log.Info(queueIsFull ? "full":"empty");
            while( true ) {
                Thread.Sleep(1000);
            }
		}

		/// <summary>
		/// Configures and runs the server.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		/// <returns>Server build descriptor.</returns>
		public static IHostBuilder CreateHostBuilder(string[] args) 
		{
			return 
				Host
					.CreateDefaultBuilder(args)
					.ConfigureWebHostDefaults(hcfg => {
						hcfg.UseKestrel();
						hcfg.ConfigureKestrel(kcfg => {
							kcfg.Listen(IPAddress.Loopback, 5000);
						});
						hcfg.UseStartup<Startup>();
					});
		}

		/// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        static void Main(string[] args)
        {
            var self = new Server();
            self.Run(args);
        }
	}
}
