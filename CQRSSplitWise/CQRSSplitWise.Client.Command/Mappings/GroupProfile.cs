using System.Collections.Generic;
using AutoMapper;

namespace CQRSSplitWise.Client.Command.Mappings
{
	public class GroupProfile : Profile
	{
		public GroupProfile()
		{
			// map RQ to command
			CreateMap<Models.BindingModel.CreateGroup, Domain.Commands.CreateGroupCmd>();

			CreateMap<Models.BindingModel.AddGroupUsers, Domain.Commands.AddGroupUsersCmd>();

			// map command to DAL
			CreateMap<Domain.Commands.CreateGroupCmd, DAL.Models.Group>()
				.ForMember(x => x.GroupId, x => x.Ignore())
				.ForMember(x => x.GroupUsers, x => x.Ignore());

			CreateMap<Domain.Commands.AddGroupUsersCmd, DAL.Models.GroupUser>()
				.ForMember(x => x.Group, x => x.Ignore())
				.ForMember(x => x.User, x => x.Ignore())
				.ForMember(x => x.UserId, x => x.Ignore());

			CreateMap<Domain.Commands.AddGroupUsersCmd, IEnumerable<DAL.Models.GroupUser>>();

			// map DAL to DTO
			CreateMap<DAL.Models.Group, Models.Dto.GroupDTO>();
		}
	}
}
