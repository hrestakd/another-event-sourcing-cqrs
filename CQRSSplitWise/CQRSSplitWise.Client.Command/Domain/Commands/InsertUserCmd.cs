using CQRSSplitWise.Client.Command.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Commands
{
	public class InsertUserCmd : IRequest<UserDTO>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
