using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserModelsResolver _modelsResolver;
    private readonly IHttpContentResolver _httpContentResolver;

    public UpdateUserCommandHandler(
        IUsersRepository userRepository,
        IUserModelsResolver modelsResolver,
        IHttpContentResolver httpContentResolver)
    {
        _usersRepository = userRepository;
        _modelsResolver = modelsResolver;
        _httpContentResolver = httpContentResolver;
    }

    public async Task<IActionResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var updateUserRequest = await _httpContentResolver.GetPayloadAsync<UpdateUserRequest>(command.Request);
        var originalDao = await _usersRepository.GetAsync(Guid.Parse(command.Id));

        var userDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updateUserRequest);
        var updatedUserDao = await _usersRepository.UpdateAsync(userDao);

        var response = _modelsResolver.GetResponseFromDao(updatedUserDao);

        return new OkObjectResult(response);
    }
}