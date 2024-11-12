using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<CreatePaymentRequest, PaymentDao>(MemberList.Source);

        CreateMap<PaymentDao, PaymentResponse>();
    }
}
