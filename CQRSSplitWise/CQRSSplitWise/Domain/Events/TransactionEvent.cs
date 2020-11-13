using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Domain.Events
{
	public class TransactionEvent : EventBase
	{
		private readonly string _eventType = "TransactionCreated";

		public TransactionEvent(object eventData, object eventMetadata)
		{
			EventID = Guid.NewGuid();
			EventType = _eventType;

			EventData = JsonConvert.SerializeObject(eventData, Formatting.Indented);
			EventMetadata = JsonConvert.SerializeObject(eventMetadata, Formatting.Indented);
		}
	}
}
