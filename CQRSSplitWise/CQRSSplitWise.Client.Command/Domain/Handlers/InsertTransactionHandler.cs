using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.Client.Command.DAL.Context;
using CQRSSplitWise.Client.Command.Domain.Commands;
using CQRSSplitWise.Client.Command.Models.Dto;
using CQRSSplitWise.DataContracts.Enums;
using CQRSSplitWise.DataContracts.Events;
using EventStore.Client;
using EventStoreDB.Extensions;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Handlers
{
	public class InsertTransactionHandler : IRequestHandler<InsertTransactionCmd, TransactionDTO>
	{
		private readonly SplitWiseSQLContext _dbContext;
		private readonly IMapper _mapper;
		private readonly EventStoreClient _eventStoreClient;

		public InsertTransactionHandler(
			SplitWiseSQLContext dbContext,
			IMapper mapper,
			EventStoreClient eventStoreClient)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_eventStoreClient = eventStoreClient;
		}

		public async Task<TransactionDTO> Handle(InsertTransactionCmd request, CancellationToken cancellationToken)
		{
			var transactionDate = DateTime.UtcNow;
			var eventDefinition = new EventDefinition<CreateTransactionEvent, EventMetadataBase>(
				EventTypes.CreateTransaction.ToString(),
				new CreateTransactionEvent(
					request.SourceUserId,
					request.DestUserID,
					transactionDate,
					request.Description,
					request.Amount
				),
				null);

			await _eventStoreClient.AppendToStreamAsync(EventStreams.Transactions.ToString(), eventDefinition);

			var transactionDto = new TransactionDTO
			{
				Amount = request.Amount,
				Description = request.Description,
				DestUserID = request.DestUserID,
				SourceUserId = request.SourceUserId,
				TransactionDate = transactionDate
			};

			return transactionDto;
		}
	}
}
