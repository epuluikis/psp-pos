using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Refund;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Models.Responses.Refund;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class RefundProfile : Profile
{
    public RefundProfile()
    {
        CreateMap<CreateRefundRequest, RefundDao>(MemberList.Source);

        CreateMap<RefundDao, RefundResponse>();
    }
}
