using System;
using System.Collections.Generic;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;

namespace Intercom.Core
{
	public class Models
	{
		public virtual string type { get; set; }
		public virtual Pages pages { get; set; }
		public virtual int total_count { get; set; }
		public virtual string scroll_param { get; set; }

		public Models ()
		{
		}
	}
}