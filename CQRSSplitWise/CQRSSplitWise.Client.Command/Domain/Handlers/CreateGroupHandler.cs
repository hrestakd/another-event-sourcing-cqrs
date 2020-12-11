using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using CQRSSplitWise.Client.Command.DAL.Context;
using CQRSSplitWise.Client.Command.Domain.Commands;
using CQRSSplitWise.Client.Command.Models.Dto;
using EventStoreDB.Extensions;
using CQRSSplitWise.DataContracts.Events;
using CQRSSplitWise.DataContracts.Enums;
using EventStore.Client;

namespace CQRSSplitWise.Client.Command.Domain.Handlers
{
	public class CreateGroupHandler : IRequestHandler<CreateGroupCmd, GroupDTO>
	{
		private readonly SplitWiseSQLContext _dbContext;
		private readonly IMapper _mapper;
		private readonly EventStoreClient _eventStoreClient;

		public CreateGroupHandler(
			SplitWiseSQLContext dbContext,
			IMapper mapper,
			EventStoreClient eventStoreClient)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_eventStoreClient = eventStoreClient;
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

			var eventDefinition = new EventDefinition<GroupCreatedEvent, EventMetadataBase>(
				EventTypes.GroupCreated.ToString(),
				new GroupCreatedEvent(
					group.GroupId,
					request.Name,
					request.GroupUserIds
				),
				null);

			await _eventStoreClient.AppendToStreamAsync(EventStreams.Groups.ToString(), eventDefinition);

			var groupDto = _mapper.Map<GroupDTO>(group);

			return groupDto;
		}
	}
}
