using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using RestSharp;

namespace Library.Exceptions
{
	public class IntercomException : Exception
	{
        public IntercomException ()
            :base()
        {
        }

        public IntercomException (String message) 
            :base(message)
        {
        }

        public IntercomException (String message, Exception innerException) 
            :base(message, innerException)
        {
        }
	}
}