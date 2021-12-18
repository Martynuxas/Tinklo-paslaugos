using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Threading;
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using NLog;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Server
{
	/// <summary>
	/// Application entry point.
	/// </summary>
	public class Server 
	{
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

		/// <summary>
		/// Program body.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		void Run(string[] args)
		{
			//configure logging
            ConfigureLogging();

			//configure and start the server
			CreateHostBuilder(args).Build().RunAsync();

						
			//suspend the main thread
			log.Info("Grpc Server has started.");
            HttpClient Client = new HttpClient();
			
			//Starting infinite loop
			while( true ) {
		
				//Main thread sleeps for 2 seconds so it doesen't spam the console
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
