using System;

public class NotEnoughMoneyException : Exception {

    public NotEnoughMoneyException()
    {
    }

    public NotEnoughMoneyException(string message) : base(message)
    {
    }

    public NotEnoughMoneyException(string message, Exception inner) : base(message, inner)
    {
    }
}
