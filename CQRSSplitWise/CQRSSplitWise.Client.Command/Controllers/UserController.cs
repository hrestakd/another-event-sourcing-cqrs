using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.Client.Command.Domain.Commands;
using CQRSSplitWise.Client.Command.Models.BindingModel;
using CQRSSplitWise.Client.Command.Models.Dto;
using EventSourcing.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Client.Command.Controllers
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
		public async Task<UserDTO> Create(InsertUser request)
		{
			var cmd = _mapper.Map<InsertUserCmd>(request);
			var result = await _mediator.Send(cmd);

			return null;
		}

    }
}