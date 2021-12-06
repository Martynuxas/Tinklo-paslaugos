using System;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Server 
{
	/// <summary>
	/// Service contract.
	/// </summary>	
	public interface IService
	{
		/// <summary>
		/// Check if gas station tank has the amount of gas.
		/// </summary>
		/// <param name="amount">Amount of gas that need</param>
		/// <returns>Result that available or not</returns>
		bool CheckTank(double amount);
		/// <summary>
		/// Add reputation to gas station
		/// </summary>
		/// <param name="amount">Amount of reputation.</param>
		/// <returns>reputation amount.</returns>
		double GiveReputation(double amount);
		/// <summary>
		/// Take gas from gas station tank
		/// </summary>
		/// <param name="amount">Amount of gas.</param>
		/// <returns>tank amount.</returns>
		double RemoveGasAmount(double amount);
		/// <summary>
		/// Add one tanker of gas to gas station
		/// </summary>
		/// <param name="amount">Amount of gas that will be filled</param>
		/// <returns>Tank amount</returns>
		double FillGasStation(double amount);
		/// <summary>
		/// Check if gas station needs gas.
		/// </summary>
		/// <returns>true - need to fill, false - no.</returns>
		double CheckForFilling();
		/// <summary>
		/// Check if queue is full
		/// </summary>
		/// <returns>true - full, false - no.</returns>
		bool CheckQueue();
		/// <summary>
		/// Set queue to full or empty
		/// </summary>
		/// <param name="value">Value to set, true - full, false - empty</param>
		/// <returns>true - full, false - empty.</returns>
		bool SetQueue(bool value);
	}
}