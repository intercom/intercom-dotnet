using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;

namespace Intercom.Exceptions
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