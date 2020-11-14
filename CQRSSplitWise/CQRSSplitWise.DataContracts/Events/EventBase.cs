using System;
using Newtonsoft.Json;

namespace CQRSSplitWise.DataContracts.Events
{
	[Serializable]
	public abstract class EventBase
	{
		public Guid EventID { get; protected set; }
		public string EventType { get; protected set; }
		protected string EventData { get; set; }
		protected string EventMetadata { get; set; }

		public T GetEventData<T>()
		{
			var eventData = JsonConvert.DeserializeObject<T>(EventData);
			return eventData;
		}

		public T GetEventMetadata<T>()
		{
			var eventMetadata = JsonConvert.DeserializeObject<T>(EventMetadata);
			return eventMetadata;
		}
	}
}
