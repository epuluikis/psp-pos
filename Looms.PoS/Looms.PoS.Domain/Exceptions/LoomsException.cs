namespace Looms.PoS.Domain.Exceptions;

public class LoomsException : Exception
{
    public LoomsException()
    {
    }

    public LoomsException(string message) : base(message)
    {
    }
}
