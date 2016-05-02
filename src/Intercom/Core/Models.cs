using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;
 
using System.Collections.Generic;

namespace Intercom.Core
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