using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;


namespace Server
{
	/// <summary>
	/// Service. Class must be marked public, otherwise ASP.NET core runtime will not find it.
	/// </summary>
	[Route("/service")] [ApiController]
	public class ServiceController : ControllerBase
	{
		/// <summary>
		/// Service logic. Use a singleton instance, since controller is instance-per-request.
		/// </summary>
		ServiceLogic logic = ServiceLogic.GetClientLogic();
		/// <summary>
		/// Check if gas station tank has the amount of gas.
		/// </summary>
		/// <param name="amount">Amount of gas that need</param>
		/// <returns>Result that available or not</returns>
		[HttpGet]
		[Route("CheckTank")]
		public ActionResult<bool> CheckTank([FromQuery] double amount)
		{
			lock( logic )
			{
				return logic.CheckTank(amount);
			}
		}
		/// <summary>
		/// Get gas station gas amount
		/// </summary>
		/// <returns>Gas station gas amount.</returns>
		[HttpGet]
		[Route("GetTankAmount")]
		public ActionResult<double> GetTankAmount()
		{
			lock( logic )
			{
				return logic.GetTankAmount();
			}
		}
		/// <summary>
		/// Check if gas station needs gas.
		/// </summary>
		/// <returns>true - need to fill, false - no.</returns>
		[HttpGet]
		[Route("CheckForFilling")]
		public ActionResult<double> CheckForFilling()
		{
			lock( logic )
			{
				return logic.CheckForFilling();
			}
		}
		/// <summary>
		/// Add one tanker of gas to gas station
		/// </summary>
		/// <param name="amount">Amount of gas that will be filled</param>
		/// <returns>Tank amount</returns>
		[HttpPost]
		[Route("FillGasStation")]
		public ActionResult<double> FillGasStation([FromQuery] double amount)
		{
			lock( logic )
			{
				return logic.FillGasStation(amount);
			}
		}
		/// <summary>
		/// Take gas from gas station tank
		/// </summary>
		/// <param name="amount">Amount of gas.</param>
		/// <returns>tank amount.</returns>
		[HttpPost]
		[Route("RemoveGasAmount")]
		public ActionResult<double> RemoveGasAmount([FromQuery] double amount)
		{
			lock( logic )
			{
				return logic.RemoveGasAmount(amount);
			}
		}
		/// <summary>
		/// Add reputation to gas station
		/// </summary>
		/// <param name="amount">Amount of reputation.</param>
		/// <returns>reputation amount.</returns>
		[HttpPost]
		[Route("GiveReputation")]
		public ActionResult<double> GiveReputation([FromQuery] double amount)
		{
			lock( logic )
			{
				return logic.GiveReputation(amount);
			}
		}
		/// <summary>
		/// Get reputation amount.
		/// </summary>
		/// <returns>reputation amount.</returns>
		[HttpGet]
		[Route("GetReputationAmount")]
		public ActionResult<double> GetReputationAmount()
		{
			lock( logic )
			{
				return logic.GetReputationAmount();
			}
		}
		/// <summary>
		/// Set queue to full or empty
		/// </summary>
		/// <param name="value">Value to set, true - full, false - empty</param>
		/// <returns>true - full, false - empty.</returns>
		[HttpPost]
		[Route("SetQueue")]
		public ActionResult<bool> SetQueue([FromQuery] bool value)
		{
			lock( logic )
			{
				return logic.SetQueue(value);
			}
		}
		/// <summary>
		/// Check if queue is full
		/// </summary>
		/// <returns>true - full, false - no.</returns>
		[HttpGet]
		[Route("CheckQueue")]
		public ActionResult<bool> CheckQueue()
		{
			lock( logic )
			{
				return logic.CheckQueue();
			}
		}
		
	}
}