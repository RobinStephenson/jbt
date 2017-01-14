using System;

public class NotEnoughMoneyException : TransactionException {

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
