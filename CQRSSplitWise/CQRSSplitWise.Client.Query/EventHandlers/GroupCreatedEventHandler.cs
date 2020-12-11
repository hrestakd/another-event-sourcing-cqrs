using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.DataContracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.EventHandlers
{
	public class GroupCreatedEventHandler
	{
		private readonly IInsertRepository<GroupData> _groupRepository;
		private readonly IQueryRepository<UserData> _userRepository;

		public GroupCreatedEventHandler(
			IInsertRepository<GroupData> groupRepository,
			IQueryRepository<UserData> userRepository)
		{
			_groupRepository = groupRepository;
			_userRepository = userRepository;
		}

		public async Task HandleGroupCreatedEvent(GroupCreatedEvent groupCreated)
		{
			if (groupCreated == null
				|| string.IsNullOrWhiteSpace(groupCreated.GroupName))
			{
				return;
			}

			IEnumerable<UserData> users = null;
			if (groupCreated.UserIDs != null && groupCreated.UserIDs.Any())
			{
				var expressions = new List<Expression<Func<UserData, bool>>>
				{
					x => true
				};

				expressions.Add(x => groupCreated.UserIDs.Contains(x.UserID));

				users = await _userRepository.GetData(expressions);
			}

			await _groupRepository
				.InsertData(new GroupData
				{
					GroupID = groupCreated.GroupID,
					GroupName = groupCreated.GroupName,
					UsersInGroup = users
				});

			return;
		}
	}
}
