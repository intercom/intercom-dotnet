using System;
using RestSharp;

namespace Library
{
	public class IntercomException : Exception
	{
		public int StatusCode { set; get; }
		public String StatusDescription { set; get; }
		public String ApiResponseBody { set; get; }
		public Errors ApiErrors { set; get; }

		public IntercomException ()
			:base()
		{
		}

		public IntercomException (String message, Exception innerException) 
			:base(message, innerException)
		{
		}

		public IntercomException (int statusCode, String statusDescription, Errors apiErrors, String apiResponseBody)
			:base()
		{
			this.StatusCode = statusCode;
			this.StatusDescription = statusDescription;
			this.ApiErrors = apiErrors;
			this.ApiResponseBody = apiResponseBody;
		}
	}
}