using System;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Server 
{

	/// <summary>
	/// Service contract.
	/// </summary>
	[ServiceContract]
	public interface IService
	{
		/// <summary>
		/// Get reputation amount.
		/// </summary>
		/// <returns>reputation amount.</returns>
		[OperationContract] double CheckForFilling();
		/// <summary>
		/// Add one tanker of gas to gas station
		/// </summary>
		/// <param name="amount">Amount of gas that will be filled</param>
		/// <returns>Tank amount</returns>
		[OperationContract] double FillGasStation(double amount);

	}
}