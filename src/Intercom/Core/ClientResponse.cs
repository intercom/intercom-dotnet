using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Net.Http;

namespace Intercom.Core
{
	public class ClientResponse<T> where T : class
	{
		public T Result { private set; get; }
		public Errors Errors { private set; get; }
		public HttpResponseMessage Response { private set; get; }

		public ClientResponse(HttpResponseMessage response, T result = null, Errors errors = null)
		{
			this.Response = response;
			this.Result = result;
			this.Errors = errors;
		}
	}
}