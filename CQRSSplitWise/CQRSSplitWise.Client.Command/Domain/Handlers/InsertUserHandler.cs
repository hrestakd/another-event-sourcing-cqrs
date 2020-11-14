using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.Client.Command.DAL.Context;
using CQRSSplitWise.Client.Command.DAL.Models;
using CQRSSplitWise.Client.Command.Domain.Commands;
using CQRSSplitWise.Client.Command.Models.Dto;
using MediatR;

namespace CQRSSplitWise.Client.Command.Domain.Handlers
{
	public class InsertUserHandler : IRequestHandler<InsertUserCmd, UserDTO>
	{
		private readonly SplitWiseSQLContext _dbContext;
		private readonly IMapper _mapper;

		public InsertUserHandler(IMapper mapper, SplitWiseSQLContext context)
		{
			_mapper = mapper;
			_dbContext = context;
		}
		public async Task<UserDTO> Handle(InsertUserCmd request, CancellationToken cancellationToken)
		{
			var user = _mapper.Map<User>(request);
			_dbContext.Users.Add(user);

			await _dbContext.SaveChangesAsync();

			var userDto = _mapper.Map<UserDTO>(user);

			return userDto;
		}
	}
}
