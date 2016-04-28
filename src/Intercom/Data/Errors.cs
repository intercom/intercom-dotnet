using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections;
using System.Collections.Generic;

namespace Library.Data
{
	public class Errors
	{
		public string type { get; set; }
		public string request_id { get; set; }
		public List<Error> errors { get; set; }
	}
}

