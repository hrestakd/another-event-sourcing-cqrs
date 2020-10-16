using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.Models.BindingModel;
using CQRSSplitWise.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TransactionController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;

		public TransactionController(IMediator mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<Transaction> Insert(InsertTransaction request)
		{
			var cmd = _mapper.Map<Domain.Commands.InsertTransactionCmd>(request);
			var result = await _mediator.Send(cmd);

			return result;
		}
	}
}
