using System;
using System.Threading;

using NLog;

using Common.Util;


namespace Server
{
	/// <summary>
	/// Server
	/// </summary>
	class Server
	{
		/// <summary>
		/// Logger for this class.
		/// </summary>
		private Logger log = LogManager.GetCurrentClassLogger();
		public static double tank { get; set; } = 0.0;//for gas station tank
		public static double reputation { get; set; } = 0.0;//for gas station reputation
		public static double fillingLevel { get; set; } = 0.0;//for gas station amount of tanker needed
		public static bool queueIsFull { get; set; } = false;//for gas station queue
		/// <summary>
		/// Program body.
		/// </summary>
		private void Run() {
			//configure logging
			LoggingUtil.ConfigureNLog();

			while( true )
			{
				try 
				{
					//start service
					var service = new Service();
					var random = new Random();
					tank = random.Next(100, 1000);

					//
					log.Info("Server has been started.");

					Console.Write($"Gas station has {tank}l of fuel\n");
					Console.Write($"Gas station has {reputation} reputation\n");
					Console.Write("Gas station queue is ");
					Console.Write(queueIsFull ? "full" : "empty");

					//hang main thread						
					while ( true ) {
						Thread.Sleep(1000);
					}
				}
				catch( Exception e ) 
				{
					//log exception
					log.Error(e, "Unhandled exception caught. Server will now restart.");

					//prevent console spamming
					Thread.Sleep(2000);
				}
			}
		}

		/// <summary>
		/// Program entry point.
		/// </summary>
		/// <param name="args">Command line arguments.</param>
		static void Main(string[] args)
		{
			var self = new Server();
			self.Run();
		}
	}
}
