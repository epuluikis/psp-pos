namespace Looms.PoS.Domain.Exceptions;

public class LoomsNotFoundException : LoomsException
{
    public LoomsNotFoundException(string message) : base(message)
    {
    }
}
