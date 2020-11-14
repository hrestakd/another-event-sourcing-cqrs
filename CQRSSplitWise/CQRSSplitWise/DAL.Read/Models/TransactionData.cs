﻿using System;
using CQRSSplitWise.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class TransactionData
	{
		public int GroupID { get; set; }
		public DateTime TransactionDate { get; set; }
		// Source/Dest wallet will not be used atm
		public string SourceWalletName { get; set; }
		public string DestWalletName { get; set; }
		[BsonRepresentation(BsonType.Decimal128)]
		public decimal Amount { get; set; }
		public string Description { get; set; }
		public TransactionType TransactionType { get; set; }
	}
}
