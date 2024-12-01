using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserModelsResolver _modelsResolver;

    public GetUserQueryHandler(IUsersRepository usersRepository, IUserModelsResolver modelsResolver)
    {
        _usersRepository = usersRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var userDao = await _usersRepository.GetAsync(Guid.Parse(query.Id));

        var response = _modelsResolver.GetResponseFromDao(userDao);

        return new OkObjectResult(response);
    }
}
