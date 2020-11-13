using AutoMapper;

namespace CQRSSplitWise.Mappings
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			// map RQ to command
			CreateMap<Models.BindingModel.InsertUser, Domain.Commands.InsertUserCmd>();
			// map command to DAL
			CreateMap<Domain.Commands.InsertUserCmd, DAL.Models.User>()
				.ForMember(x => x.UserId, x => x.Ignore())
				.ForMember(x => x.Wallet, x => x.Ignore())
				.ForMember(x => x.GroupUsers, x => x.Ignore());
			// map DAL to DTO
			CreateMap<DAL.Models.User, Models.Dto.User>();
		}
	}
}
