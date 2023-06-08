
using System.Net;
using OnlineVoting.Application.Exceptions;

namespace SystemOfWidget.Application.Common.Exceptions
{
    public class BadRequestException : Exception, IRestException
    {
        public int Code => (int)HttpStatusCode.BadRequest;

        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
