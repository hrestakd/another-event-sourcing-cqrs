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
		private readonly IQueryRepository<GroupHistory> _repository;

		public GroupHistoryService(IQueryRepository<GroupHistory> repository)
		{
			_repository = repository;
		}
	}
}
