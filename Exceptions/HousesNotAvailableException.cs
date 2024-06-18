using System;

namespace SharpAstrology.Exceptions;

public sealed class HousesNotAvailableException : Exception
{
    public HousesNotAvailableException()
    {
    }
    
    public HousesNotAvailableException(string message) 
        : base(message) 
    {
    }

    public HousesNotAvailableException(string message, Exception inner) 
        : base(message, inner) 
    {
    }
}