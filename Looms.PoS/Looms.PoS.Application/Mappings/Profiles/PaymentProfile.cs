using AutoMapper;
using Looms.PoS.Application.Models.Requests.Payment;
using Looms.PoS.Application.Models.Responses.Payment;
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
