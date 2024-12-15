using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Responses.Auth;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Looms.PoS.Application.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IAuthModelsResolver _authModelsResolver;

    public TokenService(IConfiguration configuration, IAuthModelsResolver authModelsResolver)
    {
        _configuration = configuration;
        _authModelsResolver = authModelsResolver;
    }

    public LoginResponse CreateToken(UserDao userDao)
    {
        var expires = DateTime.UtcNow.AddHours(TokenConstants.TokenDurationInHours);
        var userRole = userDao.Role;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, userDao.Id.ToString()),
                new Claim(ClaimTypes.Role, userRole.ToString()),
                new Claim(TokenConstants.BusinessIdClaim, userDao.BusinessId.ToString()),
            ]),
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return _authModelsResolver.GetResponse(tokenHandler.WriteToken(token), expires, userRole, userDao.BusinessId);
    }

    public (string BusinessId, UserRole UserRole) GetTokenData(string token)
    {
        var claims = ValidateToken(token, out var validatedToken);

        var businessId = claims.Claims.First(x => x.Type == TokenConstants.BusinessIdClaim).Value;
        var userRole = claims.Claims.First(x => x.Type == ClaimTypes.Role).Value;

        return (businessId, Enum.Parse<UserRole>(userRole));
    }

    public bool IsTokenValid(string token)
    {
        try
        {
            ValidateToken(token, out var validatedToken);

            return validatedToken.ValidTo > DateTime.UtcNow;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private ClaimsPrincipal ValidateToken(string token, out SecurityToken validatedToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        return tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!))
        };
    }
}
