namespace Looms.PoS.Domain.Exceptions;

public class LoomsForbiddenException : LoomsException
{
    public LoomsForbiddenException()
    {
    }

    public LoomsForbiddenException(string message) : base(message)
    {
    }
}
