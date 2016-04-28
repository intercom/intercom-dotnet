using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;
 
using System.Collections.Generic;

namespace Library.Core
{
	public class Models
	{
		public virtual string type { get; set; }
		public virtual Pages pages { get; set; }
		public virtual int total_count { get; set; }

		public Models ()
		{
		}
	}
}