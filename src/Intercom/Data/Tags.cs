using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;

namespace Library.Data
{
	public class Tags
	{
		public string type { get; set; }
		public List<Tag> tags { get; set; }
	}
}

