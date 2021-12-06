using System;
using System.Threading;
using NLog;


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

		readonly ReaderWriterLock _lock = new();

		/// <summary>
		/// Check if gas station tank has the amount of gas.
		/// </summary>
		/// <param name="amount">Amount of gas that need</param>
		/// <returns>Result that available or not</returns>
		public bool CheckTank(double amount)
		{
			try
			{
				_lock.AcquireWriterLock(-1);
				if (Server.tank >= amount) return true;
				else
				{
					Server.fillingLevel = 1 + (int)Math.Round((100 - Convert.ToDouble(Server.reputation)) / 50);
					return false;
				}
			}
			catch (Exception)
			{
				log.Info("Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}

		/// <summary>
		/// Add reputation to gas station
		/// </summary>
		/// <param name="amount">Amount of reputation.</param>
		/// <returns>reputation amount.</returns>
		public double GiveReputation(double amount)
		{
			try
			{
				_lock.AcquireWriterLock(-1);
				double addRes = Server.reputation += amount;
				if (0 < (addRes) && (addRes) < 100) Server.reputation = addRes;
				if ((addRes) > 100) Server.reputation = 100;
				if ((addRes) < 0) Server.reputation = 0;
				log.Info($"Reputation added to gas station {amount}");
				return Server.reputation;
			}
			catch (Exception)
			{
				log.Info("Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}

		/// <summary>
		/// Take gas from gas station tank
		/// </summary>
		/// <param name="amount">Amount of gas.</param>
		/// <returns>tank amount.</returns>
		public double RemoveGasAmount(double amount)
		{
			try
			{
				_lock.AcquireWriterLock(-1);
				Server.tank -= amount;
				log.Info($"Removed amount from gas station:{amount} l");
				return Server.tank;
			}
			catch (Exception)
			{
				log.Info("Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}
		/// <summary>
		/// Add one tanker of gas to gas station
		/// </summary>
		/// <param name="amount">Amount of gas that will be filled</param>
		/// <returns>Tank amount</returns>
		public double FillGasStation(double amount)
		{
			try
			{
				_lock.AcquireWriterLock(-1);
				if (Server.fillingLevel > 0)
				{
					Server.tank += amount;
					Server.fillingLevel--;
					log.Info($"Tanker filled gas station with {amount} l of gas");
				}
				return Server.tank;
			}
			catch (Exception)
			{
				log.Info("Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}

		/// <summary>
		/// Check if gas station needs gas.
		/// </summary>
		/// <returns>true - need to fill, false - no.</returns>
		public double CheckForFilling()
		{
			try
			{
				_lock.AcquireWriterLock(-1);
				return Server.fillingLevel;
			}
			catch (Exception)
			{
				log.Info("Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}
		/// <summary>
		/// Check if queue is full
		/// </summary>
		/// <returns>true - full, false - no.</returns>
		public bool CheckQueue()
		{
			try
			{
				_lock.AcquireWriterLock(-1);
				if (!Server.queueIsFull)
				{
					Server.queueIsFull = true;
					return false;
				}
				else
				{
					return true;
				}
			}
			catch (Exception)
			{
				log.Info("Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}
		/// <summary>
		/// Set queue to full or empty
		/// </summary>
		/// <param name="value">Value to set, true - full, false - empty</param>
		/// <returns>true - full, false - empty.</returns>
		public bool SetQueue(bool value)
		{
			try
			{
				_lock.AcquireWriterLock(-1);
				Server.queueIsFull = value;
				return Server.queueIsFull;
			}
			catch (Exception)
			{
				log.Info("Failed to acquire writer lock");
				throw;
			}
			finally
			{
				_lock.ReleaseWriterLock();
			}
		}

	}
}