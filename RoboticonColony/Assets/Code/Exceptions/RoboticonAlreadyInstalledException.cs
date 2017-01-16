using System;

public class RoboticonAlreadyInstalledException : Exception
{
    public RoboticonAlreadyInstalledException()
    {
    }

    public RoboticonAlreadyInstalledException(string message) : base(message)
    {
    }

    public RoboticonAlreadyInstalledException(string message, Exception inner) : base(message, inner)
    {
    }
}
