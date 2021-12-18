using System;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Common.Util;
using System.Diagnostics;

namespace Server
{
	public class Server
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		public static void Main(string[] args)
		{
			LoggingUtil.ConfigureNLog();			
			
			var builder = CreateWebHostBuilder(args);
			builder.Build().RunAsync();
				
			//suspend the main thread
			Console.WriteLine("Soap Server has started.");
            
			//Starting infinite loop
			while( true ) {							
				//Main thread sleeps for 2 seconds so it doesen't spam the console
				Thread.Sleep(1000);
			}
		}

		/// <summary>
		/// Create and configure web host builder.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		/// <returns>Builder created</returns>
		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			var builder =
				WebHost
					.CreateDefaultBuilder(args)
					.UseKestrel(options => {
						options.Listen(IPAddress.Loopback, 5020);
					})
					.UseStartup<Startup>();

			return builder;
		}
	}
}
