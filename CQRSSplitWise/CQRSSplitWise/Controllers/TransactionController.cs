using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.Models.BindingModel;
using CQRSSplitWise.Models.Dto;
using CQRSSplitWise.Services.Read;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly TestService _testService;

		public TransactionController(IMediator mediator, IMapper mapper, TestService testService)
		{
			_mediator = mediator;
			_mapper = mapper;
			_testService = testService;
		}

		[HttpPost("[action]")]
		public async Task<TransactionHistory> Insert(InsertTransaction request)
		{
			var transaction = await _testService.SaveDummyTransaction();

			return transaction;
			//var cmd = _mapper.Map<Domain.Commands.InsertTransactionCmd>(request);
			//var result = await _mediator.Send(cmd);

			//return result;
		}
	}
}
