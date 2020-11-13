using CQRSSplitWise.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Domain.Commands
{
	public class InsertUserCmd : IRequest<User>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
