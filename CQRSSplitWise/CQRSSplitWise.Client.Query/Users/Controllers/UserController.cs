using CQRSSplitWise.Client.Query.Users.DTO;
using CQRSSplitWise.Client.Query.Users.Filters;
using CQRSSplitWise.Client.Query.Users.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Users.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ControllerBase
	{
		private readonly UserService _userService;

		public UserController(UserService userService)
		{
			_userService = userService;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<UserDTO>> GetUsers()
		{
			var results = await _userService.GetUsers(new UserFilter());

			return results;
		}
	}
}
