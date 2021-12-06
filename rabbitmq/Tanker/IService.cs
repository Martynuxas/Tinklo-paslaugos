using System;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Tanker.Service
{
	/// <summary>
	/// Service contract.
	/// </summary>
	public interface IService
	{
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
	}
}