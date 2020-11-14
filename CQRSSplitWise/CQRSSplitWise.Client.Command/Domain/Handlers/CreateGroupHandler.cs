using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using CQRSSplitWise.Client.Command.DAL.Context;
using CQRSSplitWise.Client.Command.Domain.Commands;
using CQRSSplitWise.Client.Command.Models.Dto;

namespace CQRSSplitWise.Client.Command.Domain.Handlers
{
	public class CreateGroupHandler : IRequestHandler<CreateGroupCmd, GroupDTO>
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

		public async Task<GroupDTO> Handle(CreateGroupCmd request, CancellationToken cancellationToken)
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

			var groupDto = _mapper.Map<GroupDTO>(group);

			return groupDto;
		}
	}
}
