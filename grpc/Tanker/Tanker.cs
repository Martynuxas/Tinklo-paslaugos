using System;
using System.Threading;

using NLog;
using Grpc.Net.Client;

//this comes from GRPC generated code
using Services;


namespace Client
{
	/// <summary>
	/// Client example.
	/// </summary>
	class Client
	{
		/// <summary>
		/// Logger for this class.
		/// </summary>
		Logger log = LogManager.GetCurrentClassLogger();

		/// <summary>
		/// Configures logging subsystem.
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
		private void Run() {
			//configure logging
			ConfigureLogging();
			
			//run everythin in a loop to recover from connection errors
			while( true ) 
			{
				try {
					//connect to the server, get service proxy
					var channel = GrpcChannel.ForAddress("http://127.0.0.1:5000");
					var tanker = new Service.ServiceClient(channel);

					//use service
					var random = new Random();

					while( true ) 
					{
						log.Info($"Checking to fill");
						double FillingLevel = tanker.CheckForFilling(new CheckForFillingInput{}).Value;//Checking gas station if needs fuel
						if(FillingLevel > 0){//If need
							double rFuel = random.Next(50,100);//generate random amount
							var res = tanker.FillGasStation(new FillInput{Amount = rFuel}).Value;//Fill gas station with rFuel amount of gas
							log.Info($"Gas station Filled: {rFuel} l of gas-!-!-!-!-!");
						}
						log.Info("-----------------------------------------------");
				
						//prevent console spamming
						Thread.Sleep(2000);
					}
				}
				catch( Exception e) 
				{
					//log whatever exception to console
					log.Warn(e, "Unhandled exception caught. Will restart main loop.");

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
			var self = new Client();
			self.Run();
		}
	}
}
