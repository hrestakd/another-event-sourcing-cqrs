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
