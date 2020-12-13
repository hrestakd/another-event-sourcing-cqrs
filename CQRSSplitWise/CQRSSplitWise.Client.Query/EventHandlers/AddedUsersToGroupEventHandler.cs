using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DataContracts.Events;
using System.Linq.Expressions;

namespace CQRSSplitWise.Client.Query.EventHandlers
{
	public class AddedUsersToGroupEventHandler
	{
		private readonly IRepository<GroupData> _groupRepository;
		private readonly IRepository<UserData> _userRepository;

		public AddedUsersToGroupEventHandler(
			IRepository<GroupData> groupRepository,
			IRepository<UserData> userRepository)
		{
			_groupRepository = groupRepository;
			_userRepository = userRepository;
		}

		public async Task HandleAddedUserToGroupEvent(UsersAddedToGroupEvent usersAdded)
		{
			if (usersAdded == null
				|| usersAdded.GroupID <= 0
				|| usersAdded.UserIDs == null
				|| !usersAdded.UserIDs.Any())
			{
				return;
			}

			IEnumerable<UserData> users = null;

			var expressions = new List<Expression<Func<UserData, bool>>>
			{
				x => true
			};

			expressions.Add(x => usersAdded.UserIDs.Contains(x.UserID));

			users = await _userRepository.GetData(expressions);

			await _groupRepository
				.UpdateData(new GroupData
				{
					GroupID = usersAdded.GroupID,
					UsersInGroup = users
				});

			return;
		}
	}
}
