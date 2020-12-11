using CQRSSplitWise.Client.Query.DTO;
using CQRSSplitWise.Client.Query.Filters;
using CQRSSplitWise.Client.Query.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GroupController : ControllerBase
	{
		private readonly GroupService _groupService;

		public GroupController(GroupService groupService)
		{
			_groupService = groupService;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<GroupDTO>> GetGroups()
		{
			var results = await _groupService.GetUsers(new GroupFilter());

			return results;
		}
	}
}
