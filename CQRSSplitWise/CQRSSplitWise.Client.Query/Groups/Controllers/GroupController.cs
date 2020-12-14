using CQRSSplitWise.Client.Query.Groups.DTO;
using CQRSSplitWise.Client.Query.Groups.Filters;
using CQRSSplitWise.Client.Query.Groups.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Groups.Controllers
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
			var results = await _groupService.GetGroups(new GroupFilter());

			return results;
		}
	}
}
