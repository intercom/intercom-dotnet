using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;

namespace Intercom.Core
{
	public class ClientConfiguration
	{
        public String AppKey { private set; get; }
        public String AppId { private set; get; }

		public ClientConfiguration ()
		{
		}
	}
}