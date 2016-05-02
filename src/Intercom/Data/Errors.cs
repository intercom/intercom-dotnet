using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections;
using System.Collections.Generic;

namespace Intercom.Data
{
	public class Errors
	{
		public string type { get; set; }
		public string request_id { get; set; }
		public List<Error> errors { get; set; }
	}
}

