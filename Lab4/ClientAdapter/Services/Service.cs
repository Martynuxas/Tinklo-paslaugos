using System;
using System.Threading.Tasks;

using NLog;
using Grpc.Core;

//this comes from GRPC generated code
using Services;
using System.Threading;

namespace Server 
{
    /// <summary>
    /// Service. This is made to run as a singleton instance.
    /// </summary>
    public class Service : Services.Service.ServiceBase
    {
        /// <summary>
        /// Logger for this class.
        /// </summary>
        Logger log = LogManager.GetCurrentClassLogger();
        ServiceLogic logic = new ServiceLogic();
              
        /// <summary>
		/// Check if gas station tank has the amount.
		/// </summary>
		/// <param name="input">Amount of gas that needed.</param>
		/// <param name="context">Call context.</param>
		/// <returns>true - available, false - no.</returns>
		public override Task<CheckOutput> CheckTank(CheckInput input, ServerCallContext context) 
        {
            lock( logic ) {
				var result = new CheckOutput { Value = logic.CheckTank(input.Amount) };
				return Task.FromResult(result);				
            }
		}	
		/// <summary>
		/// Take gas from gas station tank
		/// </summary>
		/// <param name="input">Amount of gas.</param>
		/// <param name="context">Call context.</param>
		/// <returns>tank amount.</returns>
		public override Task<RemoveGasOutput> RemoveGasAmount(RemoveGasInput input, ServerCallContext context) 
        {
            lock( logic ) {
				var result = new RemoveGasOutput { Value = logic.RemoveGasAmount(input.Amount) };
				return Task.FromResult(result);		
            }		
		}	
		/// <summary>
		/// Add reputation to gas station
		/// </summary>
		/// <param name="input">Amount of reputation.</param>
		/// <param name="context">Call context.</param>
		/// <returns>reputation amount.</returns>
		public override Task<ReputationOutput> GiveReputation(ReputationInput input, ServerCallContext context) 
        {	
            lock( logic ) {
				var result = new ReputationOutput { Value = logic.GiveReputation(input.Amount) };
				return Task.FromResult(result);			
            }	
		}	
		/// <summary>
		/// Set queue to full or empty
		/// </summary>
		/// <param name="value">Value to set, true - full, false - empty</param>
		/// <returns>true - full, false - empty.</returns>
		public override Task<SetQueueOutput> SetQueue(SetQueueInput input, ServerCallContext context) 
        {			
            lock( logic ) {
				var result = new SetQueueOutput { Value = logic.SetQueue(input.Value) };
				return Task.FromResult(result);		
            }		
		}	
		/// <summary>
		/// Check if queue is full
		/// </summary>
		/// <returns>true - full, false - no.</returns>
		public override Task<CheckQueueOutput> CheckQueue(CheckQueueInput input, ServerCallContext context) 
        {				
            lock( logic ) {
				var result = new CheckQueueOutput { Value = logic.CheckQueue() };
				return Task.FromResult(result);	
            }			
		}	
    }


	
}