using AutoMapper;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Application.Models.Responses.User;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserRequest, UserDao>(MemberList.Source)
            .ForMember(src => src.Password, opt => opt.Ignore());

        CreateMap<UserDao, UserResponse>();
    }
}
