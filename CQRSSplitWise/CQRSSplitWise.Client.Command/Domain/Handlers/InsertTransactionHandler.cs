using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.Client.Command.DAL.Context;
using CQRSSplitWise.Client.Command.Domain.Commands;
using CQRSSplitWise.Client.Command.Models.Dto;
using CQRSSplitWise.Client.Command.Rabbit;
using CQRSSplitWise.DataContracts.Events;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Handlers
{
	public class InsertTransactionHandler : IRequestHandler<InsertTransactionCmd, TransactionDTO>
	{
		private readonly SplitWiseSQLContext _dbContext;
		private readonly IMapper _mapper;
		private readonly RabbitMQPublisher _publisher;

		public InsertTransactionHandler(
			SplitWiseSQLContext dbContext,
			IMapper mapper,
			RabbitMQPublisher publisher)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_publisher = publisher;
		}

		public async Task<TransactionDTO> Handle(InsertTransactionCmd request, CancellationToken cancellationToken)
		{
			var transaction = _mapper.Map<DAL.Models.Transaction>(request);
			transaction.DateCreated = DateTime.UtcNow;

			_dbContext.Transactions.Add(transaction);

			await _dbContext.SaveChangesAsync(cancellationToken);

			var eventData = MapTransactionEventData(transaction);
			_publisher.PublishTransactionEvent(eventData);

			var transactionDto = _mapper.Map<TransactionDTO>(transaction);

			return transactionDto;
		}

		private TransactionEventData MapTransactionEventData(DAL.Models.Transaction transaction)
		{
			// TODO: u repoe, also bolje da cachiramo usere tbh
			var sourceUser = _dbContext.Users
				.Where(x => x.UserId == transaction.UserId)
				.Select(x => new { x.FirstName, x.LastName })
				.FirstOrDefault();
			var destUser = _dbContext.Wallets
				.Where(x => x.WalletId == transaction.DestinationWalletId)
				.Select(x => new { x.User.UserId, x.User.FirstName, x.User.LastName })
				.FirstOrDefault();

			var eventData = new TransactionEventData
			{
				SourceUserId = transaction.UserId,
				SourceUserFirstName = sourceUser.FirstName,
				SourceUserLastName = sourceUser.LastName,
				DestUserId = destUser.UserId,
				DestUserFirstName = destUser.FirstName,
				DestUserLastName = destUser.LastName,
				TransactionType = transaction.TransactionType,
				DateCreated = transaction.DateCreated,
				Description = transaction.Description,
				Amount = transaction.Amount
			};

			return eventData;
		}
	}
}
