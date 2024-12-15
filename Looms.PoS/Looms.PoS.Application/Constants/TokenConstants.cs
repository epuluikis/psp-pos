namespace Looms.PoS.Application.Constants;

public static class TokenConstants
{
    public const string TokenHeader = "Authorization";
    public const string TokenPrefix = "Bearer ";

    public const string UserIdClaim = "UserIdClaim";
    public const string UserRoleClaim = "UserRoleClaim";
    public const string BusinessIdClaim = "BusinessIdClaim";

    public const int TokenDurationInHours = 1;
}
