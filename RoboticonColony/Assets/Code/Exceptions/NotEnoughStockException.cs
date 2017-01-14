using System;

public class NotEnoughStockException : Exception
{

    public NotEnoughStockException()
    {
    }

    public NotEnoughStockException(string message) : base(message)
    {
    }

    public NotEnoughStockException(string message, Exception inner) : base(message, inner)
    {
    }
}
