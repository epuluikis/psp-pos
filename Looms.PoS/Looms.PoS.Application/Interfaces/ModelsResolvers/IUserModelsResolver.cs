using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Application.Models.Responses.User;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IUserModelsResolver
{
    UserDao GetDaoFromRequest(CreateUserRequest createUserRequest, Guid businessId);
    UserDao GetDaoFromDaoAndRequest(UserDao originalDao, UpdateUserRequest updateUserRequest);
    UserDao GetDeletedDao(UserDao originalDao);
    UserResponse GetResponseFromDao(UserDao userDao);
    IEnumerable<UserResponse> GetResponseFromDao(IEnumerable<UserDao> userDaos);
}
