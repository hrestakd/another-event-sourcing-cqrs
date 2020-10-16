using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class TransactionHistory : ReadModelBase
	{
		public GroupData GroupData { get; set; }
		public UserData UserData { get; set; }
		public TransactionData TransactionData { get; set; }
	}
}
