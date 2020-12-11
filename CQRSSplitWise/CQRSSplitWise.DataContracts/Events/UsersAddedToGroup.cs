using System.Collections.Generic;

namespace CQRSSplitWise.DataContracts.Events
{
	public record UsersAddedToGroupEvent(int GroupID, IEnumerable<int> UserIDs);
}
