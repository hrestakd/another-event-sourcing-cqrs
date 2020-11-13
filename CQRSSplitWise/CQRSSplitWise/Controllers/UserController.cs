using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.Domain.Commands;
using CQRSSplitWise.Models.BindingModel;
using CQRSSplitWise.Models.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
    {
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;
		public UserController(IMapper mapper, IMediator mediator) 
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		public async Task<User> Create(InsertUser request)
		{
			var cmd = _mapper.Map<InsertUserCmd>(request);
			var result = await _mediator.Send(cmd);

			return result;
		}

    }
}