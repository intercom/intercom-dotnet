using System;
using System.Collections.Generic;

namespace Library
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