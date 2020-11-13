﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSSplitWise.DAL.Read.Views;
using CQRSSplitWise.DTO.Read;
using CQRSSplitWise.Filters.Read;
using CQRSSplitWise.Services.Read;
using Microsoft.AspNetCore.Mvc;

namespace CQRSSplitWise.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UserQueryController : ControllerBase
	{
		private readonly UserQueryService _userQueryService;

		public UserQueryController(UserQueryService userQueryService)
		{
			_userQueryService = userQueryService;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<UserHistoryDTO>> GetUserHistory(UserHistoryFilter filter)
		{
			var results = await _userQueryService.GetUserHistory(filter);

			return results;
		}

		[HttpGet("[action]")]
		public async Task<IEnumerable<UserStatusView>> GetUserState(int userID)
		{
			var userState = await _userQueryService.GetUserState(userID);

			return userState;
		}
	}
}
