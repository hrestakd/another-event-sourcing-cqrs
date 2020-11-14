using System.Collections.Generic;
using CQRSSplitWise.Client.Command.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Commands
{
	public class CreateGroupCmd : IRequest<GroupDTO>
	{
		public string Name { get; set; }
		public IEnumerable<int> GroupUserIds { get; set; }
	}
}
