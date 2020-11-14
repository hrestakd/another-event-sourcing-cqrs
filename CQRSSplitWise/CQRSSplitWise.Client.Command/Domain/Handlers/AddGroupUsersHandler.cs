﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.Client.Command.DAL.Context;
using CQRSSplitWise.Client.Command.Domain.Commands;
using CQRSSplitWise.Client.Command.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Handlers
{
	public class AddGroupUsersHandler : IRequestHandler<AddGroupUsersCmd, IEnumerable<GroupUsersDTO>>
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

		public async Task<IEnumerable<GroupUsersDTO>> Handle(AddGroupUsersCmd request, CancellationToken cancellationToken)
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
			var groupUsersDto = new List<GroupUsersDTO>();

			foreach (var groupUser in groupUsers)
			{
				groupUsersDto.Add(new GroupUsersDTO
				{
					GroupId = groupUser.GroupId
				});
			}

			return groupUsersDto;
		}
	}
}
