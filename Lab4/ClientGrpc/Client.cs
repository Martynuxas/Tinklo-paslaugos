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
					var client = new Service.ServiceClient(channel);

					//use service
					var random = new Random();

					while( true ) 
						{
							if(!client.CheckQueue(new CheckQueueInput{}).Value)//Checks if queue is empty
							{
								//var queueStatus = client.SetQueue(new SetQueueInput{Value = true}).Value;//Makes queue full
								
								double randomFuelAmount = random.Next(1,100);//random fuel amount [0;100]
								log.Info($"Comming vehicle that needs {randomFuelAmount} l fuel");
							
								if(client.CheckTank(new CheckInput{Amount = randomFuelAmount}).Value){//if gas station has the amount of gas

									var getFuelAmount = client.RemoveGasAmount(new RemoveGasInput{Amount = randomFuelAmount}).Value;//Removing the amount from gas station
									var getRep = client.GiveReputation(new ReputationInput{Amount = 1}).Value;//Adds reputation to gas station

									log.Info("Vehicle received gas+++");


									log.Info($"Gas station now has {getRep} reputation and {getFuelAmount} l of gas");
								}
								else{//if dont
									log.Info("There is no fuel in the tank!---");
									var giveRep = client.GiveReputation(new ReputationInput{Amount = -5}).Value;//Adds reputation to gas station
								}
								log.Info("-----------------------------------------------");
								var setQueue = client.SetQueue(new SetQueueInput{Value = false}).Value;//Makes queue empty
								//prevent console spamming
								Thread.Sleep(2000);
							}
						}
				}
				catch( Exception e ) 
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
