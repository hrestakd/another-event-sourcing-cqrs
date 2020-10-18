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
	public class InsertTransactionHandler : IRequestHandler<InsertTransactionCmd, Transaction>
	{
		private readonly SplitWiseSQLContext _dbContext;
		private readonly IMapper _mapper;

		public InsertTransactionHandler(
			SplitWiseSQLContext dbContext,
			IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<Transaction> Handle(InsertTransactionCmd request, CancellationToken cancellationToken)
		{
			var transaction = _mapper.Map<DAL.Models.Transaction>(request);
			transaction.DateCreated = DateTime.UtcNow;

			_dbContext.Transactions.Add(transaction);

			await _dbContext.SaveChangesAsync(cancellationToken);

			var transactionDto = _mapper.Map<Transaction>(transaction);

			return transactionDto;
		}

	}
}
