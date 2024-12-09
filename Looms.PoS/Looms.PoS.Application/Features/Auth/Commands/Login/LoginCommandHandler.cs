using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests.Auth;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IUsersRepository usersRepository, IHttpContentResolver httpContentResolver, ITokenService tokenService)
    {
        _usersRepository = usersRepository;
        _httpContentResolver = httpContentResolver;
        _tokenService = tokenService;
    }

    public async Task<IActionResult> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var loginRequest = await _httpContentResolver.GetPayloadAsync<LoginRequest>(command.Request);

        var userDao = await _usersRepository.GetByEmailAsync(loginRequest.Email);
        var loginResponse = _tokenService.CreateToken(userDao);

        return new OkObjectResult(loginResponse);
    }
}
