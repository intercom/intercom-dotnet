using System;
using System.Collections;
using System.Collections.Generic;

namespace Library
{
	public class Errors
	{
		public string type { get; set; }
		public string request_id { get; set; }
		public List<Error> errors { get; set; }
	}
}

