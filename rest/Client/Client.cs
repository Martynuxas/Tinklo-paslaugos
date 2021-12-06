using System;
using System.Threading;
using System.Net.Http;

using NLog;

using Common.Util;
using ServiceReference;


namespace Client
{
	/// <summary>
	/// Client
	/// </summary>
	class Client
	{
		/// <summary>
		/// Logger for this class.
		/// </summary>
		private Logger log = LogManager.GetCurrentClassLogger();

		readonly ReaderWriterLock _lock = new();

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
					var service = new ServiceClient(new HttpClient());

					//test service
					var rnd = new Random();
					//_lock.AcquireWriterLock(-1);
					while( true )
					{
							if(!service.CheckQueue())//Checks if queue is empty
							{
								//var queueStatus = service.SetQueue(true);//Makes queue full
								
								double randomFuelAmount = rnd.Next(1,100);//random fuel amount [0;100]
								log.Info($"Comming vehicle that needs {randomFuelAmount} l fuel");
							
								if(service.CheckTank(randomFuelAmount)){//if gas station has the amount of gas

									var removeGas = service.RemoveGasAmount(randomFuelAmount);//Removing the amount from gas station
									var giveRep = service.GiveReputation(1);//Adds reputation to gas station

									var getRep = service.GetReputationAmount();//Getting gas station reputation amount
									var getFuelAmount = service.GetTankAmount();//Get gas amount that gas station has
									log.Info("Vehicle received gas+++");


									log.Info($"Gas station now has {getRep} reputation and {getFuelAmount} l of gas");
								}
								else{//if dont
									log.Info("There is no fuel in the tank!---");
									var giveRep = service.GiveReputation(-5);//Adds reputation to gas station
								}
								log.Info("-----------------------------------------------");
								var setQueue = service.SetQueue(false);//Makes queue empty
								//prevent console spamming
								Thread.Sleep(2000);
							}
					}
				}
				catch( Exception e )
				{
					//log exceptions
					log.Error(e, "Unhandled exception caught. Restarting.");

					//prevent console spamming
					Thread.Sleep(2000);
				}
				/*finally
                {
                    _lock.ReleaseWriterLock();
                }*/
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
