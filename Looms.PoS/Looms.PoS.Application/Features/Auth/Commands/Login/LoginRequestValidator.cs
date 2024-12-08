using FluentValidation;
using Looms.PoS.Application.Models.Requests.Auth;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Looms.PoS.Application.Features.Auth.Commands.Login;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator(IUsersRepository usersRepository)
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .EmailAddress()
            .MustAsync(async (email, cancellationToken) =>
            {
                if (!await usersRepository.ExistsWithEmail(email))
                {
                    throw new LoomsUnauthorizedException("Invalid credentials");
                }
                return true;
            })
            .DependentRules(() =>
            {
                RuleFor(x => x.Password)
                    .CustomAsync(async (password, context, cancellationToken) =>
                    {
                        var user = await usersRepository.GetByEmailAsync(context.InstanceToValidate.Email);

                        var hashedPassword = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
                        if (user.Password != hashedPassword)
                        {
                            throw new LoomsUnauthorizedException("Invalid credentials");
                        }
                    });
            });
    }
}
