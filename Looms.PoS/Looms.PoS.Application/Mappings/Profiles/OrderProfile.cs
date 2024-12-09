using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Domain.Daos;


namespace Looms.PoS.Application.Mappings.Profiles;
public class OrderProfile : Profile
{
    public OrderProfile(
        IPaymentModelsResolver paymentModelsResolver,
        IOrderItemModelsResolver orderItemModelsResolver,
        IRefundModelsResolver refundModelsResolver)
    {
        CreateMap<OrderDao, OrderResponse>(MemberList.Source)
            .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Business.Name))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => paymentModelsResolver.GetResponseFromDao(src.Payments)))
            .ForMember(dest => dest.Refunds, opt => opt.MapFrom(src => refundModelsResolver.GetResponseFromDao(src.Refunds)))
            .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => orderItemModelsResolver.GetResponseFromDao(src.OrderItems)))
            .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => TotalsHelper.CalculateOrderTotal(src))) 
            .ForMember(dest => dest.AmountPaid, opt => opt.MapFrom(src => TotalsHelper.CalculatePaymentTotal(src.Payments)))
            .ForMember(dest => dest.AmountRefunded, opt => opt.MapFrom(src => TotalsHelper.CalculateRefundTotal(src.Refunds)));

        CreateMap<CreateOrderRequest, OrderDao>(MemberList.Source);

        CreateMap<UpdateOrderRequest, OrderDao>(MemberList.Source)
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}