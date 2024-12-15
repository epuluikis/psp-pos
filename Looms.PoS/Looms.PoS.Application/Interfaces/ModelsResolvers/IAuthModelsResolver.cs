using Looms.PoS.Application.Models.Responses.Auth;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IAuthModelsResolver
{
    LoginResponse GetResponse(string token, DateTime expires, UserRole role, Guid businessId);
}
