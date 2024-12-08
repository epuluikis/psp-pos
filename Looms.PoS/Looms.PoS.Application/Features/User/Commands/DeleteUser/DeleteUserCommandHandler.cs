using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserModelsResolver _modelsResolver;
    private readonly IHttpContentResolver _httpContentResolver;

    public DeleteUserCommandHandler(
        IUsersRepository userRepository,
        IUserModelsResolver modelsResolver,
        IHttpContentResolver httpContentResolver)
    {
        _usersRepository = userRepository;
        _modelsResolver = modelsResolver;
        _httpContentResolver = httpContentResolver;
    }

    public async Task<IActionResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _usersRepository.GetAsync(Guid.Parse(command.Id));

        var deletedDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _usersRepository.UpdateAsync(deletedDao);

        return new NoContentResult();
    }
}