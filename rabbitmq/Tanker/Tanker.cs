using System;
using System.Threading;
using System.Net.Http;

using NLog;

using Common.Util;
using Tanker.Service;


namespace Tanker
{
	/// <summary>
	/// Client
	/// </summary>
	class Tanker
	{
		/// <summary>
		/// Logger for this class.
		/// </summary>
		private Logger log = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Program body.
		/// </summary>
		private void Run()
		{
			LoggingUtil.ConfigureNLog();

			//main loop
			while( true )
			{
				try
				{
					//connect to server
					var tanker = new ServiceTanker();
					log.Info($"Tanker ID is '{tanker.TankerId}'");

					//test service
					var rnd = new Random();

					while (true)
					{
						log.Info($"Checking to fill");
						double FillingLevel = tanker.CheckForFilling();//Checking gas station if needs fuel
						if (FillingLevel > 0)
						{//If need
							double rFuel = rnd.Next(100, 150);//generate random amount
							var res = tanker.FillGasStation(rFuel);//Fill gas station with rFuel amount of gas
							log.Info($"Gas station Filled: {rFuel} l of gas-!-!-!-!-!");
						}
						log.Info("-----------------------------------------------");

						//prevent console spamming
						Thread.Sleep(1000);
					}
				}
				catch( Exception e )
				{
					//log exceptions
					log.Error(e, "Unhandled exception caught. Restarting.");

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
			var self = new Tanker();
			self.Run();
		}
	}
}
