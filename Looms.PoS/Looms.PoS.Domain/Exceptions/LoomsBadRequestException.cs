namespace Looms.PoS.Domain.Exceptions;

public class LoomsBadRequestException : LoomsException
{
    public LoomsBadRequestException()
    {
    }

    public LoomsBadRequestException(string message) : base(message)
    {
    }
}
