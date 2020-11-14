using System.Collections.Generic;
using System.Threading.Tasks;
using CQRSSplitWise.Client.Query.DTO;
using CQRSSplitWise.Client.Query.Filters;
using CQRSSplitWise.Client.Query.Services;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Client.Query.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class GroupQueryController : ControllerBase
	{
		private readonly GroupQueryService _groupQueryervice;

		public GroupQueryController(GroupQueryService groupQueryervice)
		{
			_groupQueryervice = groupQueryervice;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<GroupHistoryDTO>> GetGroupHistory(GroupHistoryFilter filter)
		{
			var results = await _groupQueryervice.GetGroupHistory(filter);

			return results;
		}

		[HttpGet("[action]")]
		public async Task GetGroupState(int groupID)
		{
			await _groupQueryervice.GetGroupState(groupID);
		}
	}
}
