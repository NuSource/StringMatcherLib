using System;
using System.Runtime.Serialization;

namespace NuSource.StringMatcherLib.Exceptions;

[Serializable]
public class InvalidMatcherOptionsException : Exception
{
    protected InvalidMatcherOptionsException()
        : base() { }
    
    public InvalidMatcherOptionsException(string message)
        : base(message) { }

    public InvalidMatcherOptionsException(string message, Exception innerException) 
        : base(message, innerException) { }

    protected InvalidMatcherOptionsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}