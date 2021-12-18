using System;
using System.Threading;
using NLog;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Mime;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Server
{
	/// <summary>
	/// Networking independant service logic.
	/// </summary>
	public class ServiceLogic
	{

		/// <summary>
		/// Logger for this class.
		/// </summary>
		Logger log = LogManager.GetCurrentClassLogger();

		private static readonly HttpClient Client = new HttpClient();
		ServiceClient service = new ServiceClient(new HttpClient());
		/// <summary>
		/// Check if gas station tank has the amount of gas.
		/// </summary>
		/// <param name="amount">Amount of gas that need</param>
		/// <returns>Result that available or not</returns>
		public bool CheckTank(double amount)
		{
			lock(Client){
				log.Info($"Send request [ CheckTank ] |GRPC -> REST|");
				return service.CheckTank(amount);;
			}
		}

		/// <summary>
		/// Add reputation to gas station
		/// </summary>
		/// <param name="amount">Amount of reputation.</param>
		/// <returns>reputation amount.</returns>
		public double GiveReputation(double amount)
		{
			lock(Client){
				log.Info($"Send request [ GiveReputation ] |GRPC -> REST|");
				return service.GiveReputation(amount);
			}
		}

		/// <summary>
		/// Take gas from gas station tank
		/// </summary>
		/// <param name="amount">Amount of gas.</param>
		/// <returns>tank amount.</returns>
		public double RemoveGasAmount(double amount)
		{
			lock(Client){
				log.Info($"Send request [ RemoveGasAmount ] |GRPC -> REST|");
				return service.RemoveGasAmount(amount);
			}
		}
		/// <summary>
		/// Check if queue is full
		/// </summary>
		/// <returns>true - full, false - no.</returns>
		public bool CheckQueue()
		{
			lock(Client){
				log.Info($"Send request [ CheckQueue ] |GRPC -> REST|");
				return service.CheckQueue();
			}
		}
		/// <summary>
		/// Set queue to full or empty
		/// </summary>
		/// <param name="value">Value to set, true - full, false - empty</param>
		/// <returns>true - full, false - empty.</returns>
		public bool SetQueue(bool value)
		{
			lock(Client){
				log.Info($"Send request [ SetQueue ] |GRPC -> REST|");
				return service.SetQueue(value);
			}
		}
	}
}