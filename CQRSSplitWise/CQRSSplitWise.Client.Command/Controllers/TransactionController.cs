using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.Client.Command.Domain.Commands;
using CQRSSplitWise.Client.Command.Models.BindingModel;
using CQRSSplitWise.Client.Command.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Client.Command.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public TransactionController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpPost("[action]")]
		public async Task<TransactionDTO> Insert(InsertTransaction request)
		{
			var cmd = new InsertTransactionCmd
			{
				Amount = request.Amount,
				Description = request.Description,
				SourceUserId = request.SourceUserId,
				DestUserID = request.DestUserID
			};

			var result = await _mediator.Send(cmd);

			return result;
		}
	}
}
