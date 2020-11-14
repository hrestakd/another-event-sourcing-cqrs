using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.DAL.Context;
using CQRSSplitWise.Domain.Commands;
using CQRSSplitWise.Models.Dto;
using MediatR;
using System.Linq;
using System.Collections.Generic;

namespace CQRSSplitWise.Domain.Handlers
{
	public class CreateGroupHandler : IRequestHandler<CreateGroupCmd, Group>
	{
		private readonly SplitWiseSQLContext _dbContext;
		private readonly IMapper _mapper;

		public CreateGroupHandler(
			SplitWiseSQLContext dbContext,
			IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<Group> Handle(CreateGroupCmd request, CancellationToken cancellationToken)
		{
			var group = _mapper.Map<DAL.Models.Group>(request);

			_dbContext.Groups.Add(group);
			await _dbContext.SaveChangesAsync(cancellationToken);

			var groupUsers = new List<DAL.Models.GroupUser>();
			foreach (var userId in request.GroupUserIds)
			{
				groupUsers.Add(new DAL.Models.GroupUser
				{
					GroupId = group.GroupId,
					UserId = userId
				});
			}
			_dbContext.GroupUsers.AddRange(groupUsers);

			// TODO: try to use HiLo to generate IDs upfront:
			// https://www.talkingdotnet.com/use-hilo-to-generate-keys-with-entity-framework-core/
			await _dbContext.SaveChangesAsync(cancellationToken);

			var groupDto = _mapper.Map<Group>(group);

			return groupDto;
		}
	}
}
