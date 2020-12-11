using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.Client.Query.DTO;
using CQRSSplitWise.Client.Query.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Services
{
	public class GroupService
	{
		private readonly IQueryRepository<GroupData> _repository;

		public GroupService(IQueryRepository<GroupData> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<GroupDTO>> GetGroups(GroupFilter userFilter)
		{
			var expressions = GenerateExpressions(userFilter);

			IEnumerable<GroupData> groups;

			if (expressions == null || expressions.Count == 0)
			{
				groups = await _repository.GetData(null);
			}
			else
			{
				groups = await _repository.GetData(expressions);
			}

			var data = new List<GroupDTO>();
			foreach (var group in groups)
			{
				data.Add(new GroupDTO
				{
					GroupID = group.GroupID,
					GroupName = group.GroupName,
					UsersInGroup = group.UsersInGroup
						.Select(x => new UserDTO
						{
							UserID = x.UserID,
							FirstName = x.Name,
							LastName = x.LastName
						})
				});
			}

			return data;
		}

		private List<Expression<Func<GroupData, bool>>> GenerateExpressions(GroupFilter filter)
		{
			var expressions = new List<Expression<Func<GroupData, bool>>>
			{
				x => true
			};

			if (filter == null)
			{
				return expressions;
			}

			if (filter.GroupID > 0)
			{
				expressions.Add(x => x.GroupID == filter.GroupID);
			}

			if (!string.IsNullOrWhiteSpace(filter.GroupName))
			{
				expressions.Add(x => x.GroupName.Contains(filter.GroupName, StringComparison.InvariantCultureIgnoreCase));
			}

			if (filter.GroupsWithUserID > 0)
			{
				expressions.Add(x => x.UsersInGroup
					.Any(y => y.UserID == filter.GroupsWithUserID));
			}

			return expressions;
		}
	}
}
