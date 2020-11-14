using AutoMapper;

namespace CQRSSplitWise.Client.Command.Mappings
{
	public class TransactionProfile : Profile
	{
		public TransactionProfile()
		{
			// map RQ to command
			CreateMap<Models.BindingModel.InsertTransaction, Domain.Commands.InsertTransactionCmd>();
			// map command to DAL
			CreateMap<Domain.Commands.InsertTransactionCmd, DAL.Models.Transaction>()
				.ForMember(x => x.TransactionId, x => x.Ignore())
				.ForMember(x => x.DateCreated, x => x.Ignore())
				.ForMember(x => x.DestinationWallet, x => x.Ignore())
				.ForMember(x => x.SourceWallet, x => x.Ignore());
			// map DAL to DTO
			CreateMap<DAL.Models.Transaction, Models.Dto.TransactionDTO>();
		}
	}
}
