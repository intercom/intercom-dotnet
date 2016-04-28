using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;
using Library.Core;
using System.Collections;

namespace Library.Data
{
	public class Companies : Models
	{
		public List<Company> companies { set; get; }

		public Companies ()
		{
		}
	}
}