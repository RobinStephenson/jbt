using System;

public class NotEnoughItemException : Exception
{

    public NotEnoughItemException()
    {
    }

    public NotEnoughItemException(string message) : base(message)
    {
    }

    public NotEnoughItemException(string message, Exception inner) : base(message, inner)
    {
    }
}
