using MediatR;

namespace Looms.PoS.Application.Interfaces.RequestHandler;

public interface ILoomsRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    void ValidatePermissions(TRequest request);
}
