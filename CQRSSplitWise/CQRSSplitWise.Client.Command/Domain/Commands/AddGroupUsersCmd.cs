using System.Collections.Generic;
using CQRSSplitWise.Client.Command.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Commands
{
	public class AddGroupUsersCmd : IRequest<IEnumerable<GroupUsersDTO>>
	{
		public int GroupId { get; set; }
		public IEnumerable<int> UserIds { get; set; }
	}
}
