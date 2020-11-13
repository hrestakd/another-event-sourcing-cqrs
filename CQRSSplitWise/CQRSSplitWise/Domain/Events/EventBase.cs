using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Domain.Events
{
	public abstract class EventBase
	{
		protected Guid EventID { get; set; }
		protected string EventType { get; set; }
		protected string EventData { get; set; }
		protected string EventMetadata { get; set; }
	}
}
