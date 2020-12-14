﻿using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DAL.Repositories;
using CQRSSplitWise.DataContracts.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Client.Query.Users.EventHandlers
{
	public class UserCreatedEventHandler
	{
		private readonly IRepository<UserData> _repository;

		public UserCreatedEventHandler(IRepository<UserData> repository)
		{
			_repository = repository;
		}

		public async Task HandleUserCreatedEvent(UserCreatedEvent userCreated)
		{
			if (userCreated == null
				|| string.IsNullOrWhiteSpace(userCreated.FirstName)
				|| string.IsNullOrWhiteSpace(userCreated.LastName))
			{
				return;
			}

			await _repository
				.InsertData(new UserData
				{
					UserID = userCreated.UserID,
					Name = userCreated.FirstName,
					LastName = userCreated.LastName
				});

			return;
		}
	}
}
