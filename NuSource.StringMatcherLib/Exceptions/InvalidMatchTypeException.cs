using System;
using System.Runtime.Serialization;

namespace NuSource.StringMatcherLib.Exceptions;

[Serializable]
public class InvalidMatchTypeException : Exception
{
    protected InvalidMatchTypeException()
        : base() { }
    
    public InvalidMatchTypeException(string message)
        : base(message) { }

    public InvalidMatchTypeException(string message, Exception innerException) 
        : base(message, innerException) { }

    protected InvalidMatchTypeException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}