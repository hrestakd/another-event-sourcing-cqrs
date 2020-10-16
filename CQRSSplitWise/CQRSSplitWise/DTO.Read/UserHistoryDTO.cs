using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.Models.Enums;

namespace CQRSSplitWise.DTO.Read
{
	public class UserHistoryDTO
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime TransactionDate { get; set; }
		public string SourceWalletName { get; set; }
		public string DestWalletName { get; set; }
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public TransactionType TransactionType { get; set; }
	}
}
