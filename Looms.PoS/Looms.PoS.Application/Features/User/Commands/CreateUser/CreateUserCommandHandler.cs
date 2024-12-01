using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserModelsResolver _modelsResolver;
    private readonly IHttpContentResolver _httpContentResolver;

    public CreateUserCommandHandler(
        IUsersRepository userRepository,
        IUserModelsResolver modelsResolver,
        IHttpContentResolver httpContentResolver)
    {
        _usersRepository = userRepository;
        _modelsResolver = modelsResolver;
        _httpContentResolver = httpContentResolver;
    }

    public async Task<IActionResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userRequest = await _httpContentResolver.GetPayloadAsync<CreateUserRequest>(command.Request);

        var userDao = _modelsResolver.GetDaoFromRequest(userRequest);
        var createdUserDao = await _usersRepository.CreateAsync(userDao);

        var response = _modelsResolver.GetResponseFromDao(createdUserDao);

        return new CreatedAtRouteResult($"/users/{createdUserDao.Id}", response);
    }
}
