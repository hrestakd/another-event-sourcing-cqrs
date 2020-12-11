using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.Client.Query.DTO;
using CQRSSplitWise.Client.Query.Filters;
using CQRSSplitWise.DataContracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Services
{
	public class UserService
	{
		private readonly IQueryRepository<UserData> _repository;

		public UserService(IQueryRepository<UserData> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<UserDTO>> GetUsers(UserFilter userFilter)
		{
			var expressions = GenerateExpressions(userFilter);

			IEnumerable<UserData> users;

			if (expressions == null || expressions.Count == 0)
			{
				users = await _repository.GetData(null);
			}
			else
			{
				users = await _repository.GetData(expressions);
			}

			var data = new List<UserDTO>();
			foreach (var user in users)
			{
				data.Add(new UserDTO
				{
					UserID = user.UserID,
					FirstName = user.Name,
					LastName = user.LastName
				});
			}

			return data;
		}

		private List<Expression<Func<UserData, bool>>> GenerateExpressions(UserFilter filter)
		{
			var expressions = new List<Expression<Func<UserData, bool>>>
			{
				x => true
			};

			if (filter == null)
			{
				return expressions;
			}

			if (filter.UserID > 0)
			{
				expressions.Add(x => x.UserID == filter.UserID);
			}

			if (!string.IsNullOrWhiteSpace(filter.FirstName))
			{
				expressions.Add(x => x.Name.Contains(filter.FirstName, StringComparison.InvariantCultureIgnoreCase));
			}

			if (!string.IsNullOrWhiteSpace(filter.LastName))
			{
				expressions.Add(x => x.LastName.Contains(filter.LastName, StringComparison.InvariantCultureIgnoreCase));
			}

			return expressions;
		}
	}
}
