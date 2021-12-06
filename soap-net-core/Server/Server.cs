using System;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Common.Util;


namespace Server
{
	public class Server
	{
		public static double tank { get; set; } = 0;//for gas station tank
		public static double reputation { get; set; } = 0;//for gas station reputation
		public static double fillingLevel { get; set; } = 0;//for gas station amount of tanker needed
		public static bool queueIsFull { get; set; } = false;//for gas station queue

		/// <summary>
		/// Program entry point.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		public static void Main(string[] args)
		{
			var random = new Random();
			tank = random.Next(100,1000);
			
			LoggingUtil.ConfigureNLog();			
			
			var builder = CreateWebHostBuilder(args);
			builder.Build().RunAsync();

			Console.Write("Server has started.\n");
			Console.Write($"Gas station has {tank}l of fuel\n");
			Console.Write($"Gas station has {reputation} reputation\n");
			Console.Write("Gas station queue is ");
			Console.Write(queueIsFull ? "full":"empty");
			while(true)
			{
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
						options.Listen(IPAddress.Loopback, 5000);
					})
					.UseStartup<Startup>();

			return builder;
		}
	}
}
