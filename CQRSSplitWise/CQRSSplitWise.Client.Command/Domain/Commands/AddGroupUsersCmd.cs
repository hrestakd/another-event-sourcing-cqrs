using System.Collections.Generic;
using CQRSSplitWise.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Domain.Commands
{
	public class AddGroupUsersCmd : IRequest<IEnumerable<GroupUsers>>
	{
		public int GroupId { get; set; }
		public IEnumerable<int> UserIds { get; set; }
	}
}
