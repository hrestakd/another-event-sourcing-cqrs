﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class GroupHistory : ReadModelBase
	{
		public GroupData GroupData { get; set; }
		public IEnumerable<Transaction> Transactions { get; set; }
	}
}
