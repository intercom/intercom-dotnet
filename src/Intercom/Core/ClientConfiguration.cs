using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;

namespace Library.Core
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