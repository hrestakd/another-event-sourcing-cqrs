using AutoMapper;
using CQRSSplitWise.DAL.Read.Views;
using CQRSSplitWise.DTO.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSplitWise.Mappings
{
	public class UserStatusProfile : Profile
	{
		//TODO if necessary
		public UserStatusProfile()
		{
			//CreateMap<UserStatusView, UserStatusDTO>()
			//	.ForMember(x => x.Name, x => x.MapFrom(y => y.SourceUserData.Name))
			//	.ForMember(x => x.LastName, x => x.MapFrom(y => y.SourceUserData.LastName))
			//	.ForMember(x => x.Amount, x => x.MapFrom(y => y.TransactionData.Amount))
			//	.ForMember(x => x.Description, x => x.MapFrom(y => y.TransactionData.Description))
			//	.ForMember(x => x.DestWalletName, x => x.MapFrom(y => y.TransactionData.DestWalletName))
			//	.ForMember(x => x.SourceWalletName, x => x.MapFrom(y => y.TransactionData.SourceWalletName))
			//	.ForMember(x => x.TransactionDate, x => x.MapFrom(y => y.TransactionData.TransactionDate))
			//	.ForMember(x => x.TransactionType, x => x.MapFrom(y => y.TransactionData.TransactionType));
		}
	}
}
