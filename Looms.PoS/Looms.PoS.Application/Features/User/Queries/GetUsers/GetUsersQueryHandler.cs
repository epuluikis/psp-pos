using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserModelsResolver _modelsResolver;

    public GetUsersQueryHandler(IUsersRepository usersRepository, IUserModelsResolver modelsResolver)
    {
        _usersRepository = usersRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var userDaos = await _usersRepository.GetAllAsync();

        var response = _modelsResolver.GetResponseFromDao(userDaos);

        return new OkObjectResult(response);
    }
}
