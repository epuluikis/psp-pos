namespace Looms.PoS.Domain.Exceptions;

public class LoomsUnauthorizedException : LoomsException
{
    public LoomsUnauthorizedException()
    {
    }

    public LoomsUnauthorizedException(string message) : base(message)
    {
    }
}
