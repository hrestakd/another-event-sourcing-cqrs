using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class TransactionHistory
	{
		public Guid Guid { get; set; }
		public GroupData GroupData { get; set; }
		public UserData SourceUserData { get; set; }
		public UserData DestUserData { get; set; }
		public TransactionData TransactionData { get; set; }
	}
}
