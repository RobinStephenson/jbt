using System;

public class TileAlreadyOwnedException : Exception
{
    public TileAlreadyOwnedException()
    {
    }

    public TileAlreadyOwnedException(string message) : base(message)
    {
    }

    public TileAlreadyOwnedException(string message, Exception inner) : base(message, inner)
    {
    }
}
