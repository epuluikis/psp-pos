using Looms.PoS.Application.Interfaces.RequestHandler;
using MediatR;

namespace Looms.PoS.Application.Utilities.Behaviours;

public class PermissionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    private readonly IServiceProvider _serviceProvider;

    public PermissionBehaviour(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var handlerType = typeof(IRequestHandler<TRequest, TResponse>);
        var handler = _serviceProvider.GetService(handlerType) as IRequestHandler<TRequest, TResponse>;

        if (handler is ILoomsRequestHandler<TRequest, TResponse> requestHandler)
        {
            requestHandler.ValidatePermissions(request);
        }

        return next();
    }
}
