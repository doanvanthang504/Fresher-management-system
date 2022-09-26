using Global.Shared.Commons;
using System;
using System.Globalization;
using System.Net;

namespace Global.Shared.Exceptions
{
    public class AppException : Exception
    {
        public int Status { get; set; }

        public AppException() : base() { }

        public AppException(string message, int statusCode = Constant.INTERNAL_SERVER_ERROR) : base(message) 
        {
            Status = statusCode;
        }

        public AppException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
    public class AppNotFoundException : AppException
    {
        public AppNotFoundException(string message) : base(message)
        {
            Status = (int)HttpStatusCode.NotFound;
        }
    }
    public class AppArgumentInvalidException : AppException
    {
        public AppArgumentInvalidException(string message) : base(message)
        {
            Status = (int)HttpStatusCode.BadRequest;
        }
    }
    public class AppUnauthorizedException : AppException
    {
        public AppUnauthorizedException(string message= "Unauthorized !") : base(message)
        {
            Status = (int)HttpStatusCode.Unauthorized;
        }
    }
    public class AppForbiddenException : AppException
    {
        public AppForbiddenException(string message= "No Permission !") : base(message)
        {
            Status = (int)HttpStatusCode.Forbidden;
        }
    }
    public class AppDbUpdateException : AppException
    {
        public AppDbUpdateException(string message) : base(message)
        {
            Status = (int)HttpStatusCode.InternalServerError;
        }
    }
}
