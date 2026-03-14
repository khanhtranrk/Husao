namespace Sii;

public class SiiException : Exception
{
    public SiiException(string message) : base(message)
    {
    }

    public SiiException(string message, Exception inner) : base(message, inner)
    {
    }
}
