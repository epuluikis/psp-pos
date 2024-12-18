using Looms.PoS.Application.Models.Dtos;
using Looms.PoS.Application.Models.Responses.Auth;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IAuthModelsResolver
{
    LoginResponse GetResponse(string token, DateTime expires, TokenDataDto tokenDataDto);
}
