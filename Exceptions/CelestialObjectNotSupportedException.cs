using System;

namespace SharpAstrology.Exceptions;

public sealed class CelestialObjectNotSupportedException : Exception
{
    public CelestialObjectNotSupportedException()
    {
    }
    
    public CelestialObjectNotSupportedException(string message) 
        : base(message) 
    {
    }

    public CelestialObjectNotSupportedException(string message, Exception inner) 
        : base(message, inner) 
    {
    }
}