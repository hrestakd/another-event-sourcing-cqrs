using System;
using Newtonsoft.Json;

namespace CQRSSplitWise.Domain.Events
{
	[Serializable]
	public class TransactionEvent : EventBase
	{
		private readonly string _eventType = "TransactionCreated";

		public TransactionEvent(TransactionEventData eventData, object eventMetadata)
		{
			EventID = Guid.NewGuid();
			EventType = _eventType;

			if (eventData != null)
			{
				EventData = JsonConvert.SerializeObject(eventData, Formatting.Indented);
			}

			if (eventMetadata != null)
			{
				EventMetadata = JsonConvert.SerializeObject(eventMetadata, Formatting.Indented);
			}
		}
	}
}
