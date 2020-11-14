using AutoMapper;
using CQRSSplitWise.Client.Query.DAL.Models;
using CQRSSplitWise.Client.Query.DTO;

namespace CQRSSplitWise.Client.Query.AutoMapper
{
	public class UserHistoryProfile : Profile
	{
		public UserHistoryProfile()
		{
			CreateMap<TransactionHistory, UserHistoryDTO>()
				.IgnoreAllPropertiesWithAnInaccessibleSetter()
				.IgnoreAllSourcePropertiesWithAnInaccessibleSetter()
				.ForMember(x => x.Name, x => x.MapFrom(y => y.SourceUserData.Name))
				.ForMember(x => x.LastName, x => x.MapFrom(y => y.SourceUserData.LastName))
				.ForMember(x => x.Amount, x => x.MapFrom(y => y.TransactionData.Amount))
				.ForMember(x => x.Description, x => x.MapFrom(y => y.TransactionData.Description))
				.ForMember(x => x.DestWalletName, x => x.MapFrom(y => y.TransactionData.DestWalletName))
				.ForMember(x => x.SourceWalletName, x => x.MapFrom(y => y.TransactionData.SourceWalletName))
				.ForMember(x => x.TransactionDate, x => x.MapFrom(y => y.TransactionData.TransactionDate))
				.ForMember(x => x.TransactionType, x => x.MapFrom(y => y.TransactionData.TransactionType));
		}
	}
}
