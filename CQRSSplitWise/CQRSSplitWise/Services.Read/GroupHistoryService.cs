using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;

namespace CQRSSplitWise.Services.Read
{
	public class GroupHistoryService
	{
		private readonly IQueryRepository<TransactionHistory> _repository;

		public GroupHistoryService(IQueryRepository<TransactionHistory> repository)
		{
			_repository = repository;
		}
	}
}
