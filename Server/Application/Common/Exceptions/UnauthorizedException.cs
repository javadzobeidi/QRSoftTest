using System.Net;

namespace Application.Common.Exceptions;

public class UnauthorizedException : Exception
{



    public UnauthorizedException()
        : base()
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
      
    }

    public UnauthorizedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public UnauthorizedException(string name, object key)
        : base($"یافت نشده است \"{name}\" ({key})   .")
    {
    }
}
