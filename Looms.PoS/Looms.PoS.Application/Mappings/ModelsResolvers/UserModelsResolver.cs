using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Business;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Application.Models.Responses.User;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using System.Security.Cryptography;
using System.Text;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class UserModelsResolver : IUserModelsResolver
{
    private readonly IMapper _mapper;

    public UserModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public UserDao GetDaoFromRequest(CreateUserRequest createUserRequest, Guid businessId)
    {
        return _mapper.Map<UserDao>(createUserRequest) with
        {
            Id = Guid.NewGuid(),
            Password = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(createUserRequest.Password))),
            BusinessId = businessId
        };
    }

    public UserDao GetDaoFromDaoAndRequest(UserDao originalDao, UpdateUserRequest updateUserRequest)
    {
        return originalDao with
        {
            Name = updateUserRequest.Name,
            Role = Enum.Parse<UserRole>(updateUserRequest.Role)
        };
    }

    public UserDao GetDeletedDao(UserDao originalDao)
    {
        return originalDao with
        {
            IsDeleted = true
        };
    }

    public UserResponse GetResponseFromDao(UserDao userDao)
        => _mapper.Map<UserResponse>(userDao);

    public IEnumerable<UserResponse> GetResponseFromDao(IEnumerable<UserDao> userDaos)
        => _mapper.Map<IEnumerable<UserResponse>>(userDaos);

    public UserDao GetDaoFromDaoAndRequest(UserDao originalDao, UpdateBusinessRequest updateBusinessRequest)
    {
        throw new NotImplementedException();
    }
}
