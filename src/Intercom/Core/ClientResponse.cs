using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using RestSharp;

namespace Library.Core
{
	public class ClientResponse<T> where T : class
	{
		public T Result { private set; get; }
		public Errors Errors { private set; get; }
		public IRestResponse Response { private set; get; }

		public ClientResponse(IRestResponse response, T result = null, Errors errors = null)
		{
			this.Response = response;
			this.Result = result;
			this.Errors = errors;
		}
	}
}