﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.DAL.Read.Models
{
	public class GroupState : ReadModelBase
	{
		public int GroupID { get; set; }
		public string GroupName { get; set; }
		public decimal Amount { get; set; }
	}
}
