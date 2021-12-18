using System;
using System.Threading;
using NLog;
using System.Net.Http;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace Server
{
	/// <summary>
	/// Service logic.
	/// </summary>
	class ServiceLogic : IService 
	{
		/// <summary>
		/// Logger for this class.
		/// </summary>
		private Logger log = LogManager.GetCurrentClassLogger();	

		private static readonly HttpClient Client = new();
  		TankerClient service = new TankerClient(new HttpClient());
		/// <summary>
		/// Add one tanker of gas to gas station
		/// </summary>
		/// <param name="amount">Amount of gas that will be filled</param>
		/// <returns>Tank amount</returns>
		public double FillGasStation(double amount)
		{
			lock(Client){
				log.Info($"Send request [ FillGasStation ] |SOAP -> REST|");
				return service.FillGasStation(amount); 
			}
		}

		/// <summary>
		/// Check if gas station needs gas.
		/// </summary>
		/// <returns>true - need to fill, false - no.</returns>
		public double CheckForFilling()
		{
			lock(Client){
				log.Info($"Send request [ CheckForFilling ] |SOAP -> REST|");
				return service.CheckForFilling(); 
			}
		}
	}
}