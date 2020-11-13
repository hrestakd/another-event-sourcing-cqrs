using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CQRSSplitWise.DAL.Read.Models;
using CQRSSplitWise.DTO.Read;

namespace CQRSSplitWise.Mappings
{
	public class UserHistoryProfile : Profile
	{
		public UserHistoryProfile()
		{
			CreateMap<TransactionHistory, UserHistoryDTO>()
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
