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
	public class GroupController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public GroupController(IMapper mapper, IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		public async Task<Group> Create(CreateGroup request)
		{
			var cmd = _mapper.Map<CreateGroupCmd>(request);
			var result = await _mediator.Send(cmd);

			return result;
		}

		[HttpPost("[action]")]
		public async Task<IEnumerable<GroupUsers>> AddGroupUsers(AddGroupUsers request)
		{
			var cmd = _mapper.Map<AddGroupUsersCmd>(request);
			var result = await _mediator.Send(cmd);

			return result;
		}
	}
}
