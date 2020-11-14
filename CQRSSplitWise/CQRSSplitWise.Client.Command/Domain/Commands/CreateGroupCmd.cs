using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Domain.Commands
{
	public class CreateGroupCmd : IRequest<Group>
	{
		public string Name { get; set; }
		public IEnumerable<int> GroupUserIds { get; set; }
	}
}
