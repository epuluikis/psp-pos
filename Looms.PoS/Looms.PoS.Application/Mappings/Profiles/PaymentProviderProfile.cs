using AutoMapper;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Application.Models.Responses.PaymentProvider;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class PaymentProviderProfile : Profile
{
    public PaymentProviderProfile()
    {
        CreateMap<CreatePaymentProviderRequest, PaymentProviderDao>(MemberList.Source);

        CreateMap<UpdatePaymentProviderRequest, PaymentProviderDao>(MemberList.Source);

        CreateMap<PaymentProviderDao, PaymentProviderResponse>();
    }
}
