using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;

namespace Intercom.Data
{
	public class Events : Models
	{
		public List<Event> events { set; get; }

		public Events ()
		{
		}
	}
}