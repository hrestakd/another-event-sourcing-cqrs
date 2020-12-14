using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.Client.Command.DAL.Context;
using CQRSSplitWise.Client.Command.DAL.Models;
using CQRSSplitWise.Client.Command.Domain.Commands;
using CQRSSplitWise.Client.Command.Models.Dto;
using CQRSSplitWise.DataContracts.Enums;
using CQRSSplitWise.DataContracts.Events;
using EventStore.Client;
using EventStoreDB.Extensions;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Handlers
{
	public class InsertUserHandler : IRequestHandler<InsertUserCmd, UserDTO>
	{
		private readonly SplitWiseSQLContext _dbContext;
		private readonly IMapper _mapper;
		private readonly EventStoreClient _eventStoreClient;

		public InsertUserHandler(
			IMapper mapper,
			SplitWiseSQLContext context,
			EventStoreClient eventStoreClient)
		{
			_mapper = mapper;
			_dbContext = context;
			_eventStoreClient = eventStoreClient;
		}
		public async Task<UserDTO> Handle(InsertUserCmd request, CancellationToken cancellationToken)
		{
			var user = _mapper.Map<User>(request);
			_dbContext.Users.Add(user);

			await _dbContext.SaveChangesAsync();

			var eventDefinition = new EventDefinition<UserCreatedEvent,EventMetadataBase>(
				EventTypes.UserCreated.ToString(),
				new UserCreatedEvent(
					user.UserId,
					request.FirstName,
					request.LastName
				),
				null);

			await _eventStoreClient.AppendToStreamAsync(EventStreams.Users.ToString(), eventDefinition);

			var userDto = _mapper.Map<UserDTO>(user);

			return userDto;
		}
	}
}
