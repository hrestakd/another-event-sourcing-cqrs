using System;
using Microsoft.Extensions.ObjectPool;
using RabbitMQ.Client;

namespace CQRSSplitWise.Rabbit
{
	public static class RabbitExtensions
	{
		private const string TRANSACTION_QUEUE_NAME = "TransactionEvents";

		/// <summary>
		/// Declare durable TransactionEvents queue for TransactionEvents if it doesn't exist on the channel.
		/// </summary>
		/// <param name="channel"></param>
		/// <returns>Queue Name</returns>
		public static string DeclareTransactionQueue(this IModel channel)
		{
			var result = channel.QueueDeclare(
					queue: TRANSACTION_QUEUE_NAME,
					durable: true, // saves to the disk, resumes when container restarts
					exclusive: false, // false because we will want more consumers in the future
					autoDelete: false,
					arguments: null);
			
			return result.QueueName;
		}

		/// <summary>
		/// Uses a transaction channel from the object pool and executes the provided action
		/// </summary>
		/// <param name="channelPool"></param>
		/// <param name="action"></param>
		public static void UseTransactionChannel(this ObjectPool<IModel> channelPool, Action<IModel, string> action)
		{
			// get channel from the object pool
			var channel = channelPool.Get();

			try
			{
				var queueName = channel.DeclareTransactionQueue();
				action(channel, queueName);
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				// return object to the pool
				channelPool.Return(channel);
			}
		}
	}
}
