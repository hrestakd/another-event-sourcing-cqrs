using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.DAL.Context;
using CQRSSplitWise.Domain.Commands;
using CQRSSplitWise.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Domain.Handlers
{
	public class AddGroupUsersHandler : IRequestHandler<AddGroupUsersCmd, IEnumerable<GroupUsers>>
	{
		private readonly SplitWiseSQLContext _dbContext;
		private readonly IMapper _mapper;

		public AddGroupUsersHandler(
			SplitWiseSQLContext dbContext,
			IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<GroupUsers>> Handle(AddGroupUsersCmd request, CancellationToken cancellationToken)
		{
			// todo:
			//var groupUsers = _mapper.Map<IEnumerable<DAL.Models.GroupUser>>(request);

			// temp solution:
			var groupUsers = new List<DAL.Models.GroupUser>();
			foreach (var userId in request.UserIds)
			{
				var groupUser = new DAL.Models.GroupUser
				{
					GroupId = request.GroupId,
					UserId = userId
				};
				groupUsers.Add(groupUser);
			}

			_dbContext.GroupUsers.AddRange(groupUsers);

			await _dbContext.SaveChangesAsync(cancellationToken);

			// temp sln:
			var groupUsersDto = new List<GroupUsers>();

			foreach (var groupUser in groupUsers)
			{
				groupUsersDto.Add(new GroupUsers
				{
					GroupId = groupUser.GroupId
				});
			}

			return groupUsersDto;
		}
	}
}
