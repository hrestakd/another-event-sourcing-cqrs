using System.Collections.Generic;

namespace CQRSSplitWise.DataContracts.Events
{
	public record GroupCreatedEvent(int GroupID, string GroupName, IEnumerable<int> UserIDs);
}
