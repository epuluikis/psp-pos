using AutoMapper;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Application.Models.Responses.PaymentTerminal;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class PaymentTerminalProfile : Profile
{
    public PaymentTerminalProfile()
    {
        CreateMap<CreatePaymentTerminalRequest, PaymentTerminalDao>(MemberList.Source);

        CreateMap<UpdatePaymentTerminalRequest, PaymentTerminalDao>(MemberList.Source);

        CreateMap<PaymentTerminalDao, PaymentTerminalResponse>();
    }
}
