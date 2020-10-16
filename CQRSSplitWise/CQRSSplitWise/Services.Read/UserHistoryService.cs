using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read;
using CQRSSplitWise.DAL.Read.Models;

namespace CQRSSplitWise.Services.Read
{
	public class UserHistoryService
	{
		private readonly IQueryRepository<UserHistory> _repository;

		public UserHistoryService(IQueryRepository<UserHistory> repository)
		{
			_repository = repository;
		}
	}
}
