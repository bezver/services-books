using System.Net;

namespace Books.Domain.Exceptions
{
	public class ServiceException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError) : Exception(message)
	{
		public HttpStatusCode HttpStatusCode { get; set; } = httpStatusCode;
	}
}
