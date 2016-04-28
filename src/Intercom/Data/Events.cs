using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using Library.Core;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace Library.Data
{
	public class Events : Models
	{
		public List<Event> events { set; get; }

		public Events ()
		{
		}
	}
}