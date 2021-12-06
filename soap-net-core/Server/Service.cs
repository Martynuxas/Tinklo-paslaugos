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
		private ServiceLogic logic = new ServiceLogic();

		/// <summary>
		/// Check if gas station tank has the amount of gas.
		/// </summary>
		/// <param name="amount">Amount of gas that need</param>
		/// <returns>Result that available or not</returns>
		public bool CheckTank(double amount) {

			lock( logic ) {				
				return logic.CheckTank(amount);
			}
		}	
		/// <summary>
		/// Get gas station gas amount
		/// </summary>
		/// <returns>Gas station gas amount.</returns>
		public double GetTankAmount() {

			lock( logic ) {				
				return logic.GetTankAmount();
			}
		}	
		/// <summary>
		/// Check if gas station needs gas.
		/// </summary>
		/// <returns>true - need to fill, false - no.</returns>
		public double CheckForFilling() {

			lock( logic ) {				
				return logic.CheckForFilling();
			}
		}	
		/// <summary>
		/// Add one tanker of gas to gas station
		/// </summary>
		/// <param name="input">Amount of gas that will be filled.</param>
		/// <param name="context">Call context.</param>
		/// <returns>tank amount.</returns>
		public double FillGasStation(double amount) {

			lock( logic ) {				
				return logic.FillGasStation(amount);
			}
		}	
		/// <summary>
		/// Take gas from gas station tank
		/// </summary>
		/// <param name="input">Amount of gas.</param>
		/// <param name="context">Call context.</param>
		/// <returns>tank amount.</returns>
		public double RemoveGasAmount(double amount) {

			lock( logic ) {				
				return logic.RemoveGasAmount(amount);
			}
		}	
		/// <summary>
		/// Add reputation to gas station
		/// </summary>
		/// <param name="input">Amount of reputation.</param>
		/// <param name="context">Call context.</param>
		/// <returns>reputation amount.</returns>
		public double GiveReputation(double amount) {

			lock( logic ) {				
				return logic.GiveReputation(amount);
			}
		}	
		/// <summary>
		/// Get reputation amount.
		/// </summary>
		/// <returns>reputation amount.</returns>
		public double GetReputationAmount() {

			lock( logic ) {				
				return logic.GetReputationAmount();
			}
		}	
		/// <summary>
		/// Set queue to full or empty
		/// </summary>
		/// <param name="value">Value to set, true - full, false - empty</param>
		/// <returns>true - full, false - empty.</returns>
		public bool SetQueue(bool value) {

			lock( logic ) {				
				return logic.SetQueue(value);
			}
		}	
		/// <summary>
		/// Check if queue is full
		/// </summary>
		/// <returns>true - full, false - no.</returns>
		public bool CheckQueue() {

			lock( logic ) {				
				return logic.CheckQueue();
			}
		}	

		
	}
}