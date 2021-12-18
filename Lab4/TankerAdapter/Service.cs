using System;
using System.Threading;
using System.ServiceModel;


namespace Server
{
	/// <summary>
	/// Service.
	/// </summary>
	class Service : IService
	{
		
		/// <summary>
		/// Service logic.
		/// </summary>
		//Getting main logic instance from class: ForestLogic
		private ServiceLogic logic = new ServiceLogic();

		/// <summary>
		/// Add one tanker of gas to gas station
		/// </summary>
		/// <param name="amount">Amount of gas that will be filled</param>
		/// <returns>Tank amount</returns>
		public double FillGasStation(double amount)
		{
			lock(logic){
				return logic.FillGasStation(amount); 
			}
		}

		/// <summary>
		/// Check if gas station needs gas.
		/// </summary>
		/// <returns>true - need to fill, false - no.</returns>
		public double CheckForFilling()
		{
			lock(logic){
				return logic.CheckForFilling(); 
			}
		}
	}
}