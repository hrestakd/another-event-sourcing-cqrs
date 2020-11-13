using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.DAL.Context;
using CQRSSplitWise.Domain.Commands;
using CQRSSplitWise.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Domain.Handlers
{
	public class InsertUserHandler : IRequestHandler<InsertUserCmd, User>
	{
		private readonly SplitWiseSQLContext _dbContext;
		private readonly IMapper _mapper;

		public InsertUserHandler(IMapper mapper, SplitWiseSQLContext context)
		{
			_mapper = mapper;
			_dbContext = context;
		}
		public async Task<User> Handle(InsertUserCmd request, CancellationToken cancellationToken)
		{
			var user = _mapper.Map<DAL.Models.User>(request);
			_dbContext.Users.Add(user);

			await _dbContext.SaveChangesAsync();

			var userDto = _mapper.Map<User>(user);

			return userDto;
		}
	}
}
