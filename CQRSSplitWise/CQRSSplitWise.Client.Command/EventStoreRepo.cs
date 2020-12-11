using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore.Client;
using EventStoreDB.Extensions;
using Microsoft.Extensions.Logging;

namespace EventSourcing.API
{
	public class EventStoreRepo
	{
		private readonly EventStoreClient _eventStoreClient;

		public EventStoreRepo(EventStoreClient eventStoreClient)
		{
			_eventStoreClient = eventStoreClient;
		}

		public async Task CreateStream()
		{
			var newEvent = new EventDefinition<TestEventData, TestEventMetadata>(
				"testEventType",
				new TestEventData
				{
					TestDataProp = "TestDataValue"
				},
				new TestEventMetadata
				{
					TestMetadataProp = "TestMetadataValue"
				});

			await _eventStoreClient.AppendToStreamAsync("testStream", newEvent);
		}
	}

	public record TestEventData
	{
		public string TestDataProp { get; set; }
	}

	public record TestEventMetadata : EventMetadataBase
	{
		public string TestMetadataProp { get; set; }
	}
}
