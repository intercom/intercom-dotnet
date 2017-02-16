using System;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;
using Intercom.Core;
using System.Collections;

namespace Intercom.Data
{
	public class Companies : Models
	{
		public List<Company> companies { set; get; }
        public string scroll_param { get; set; }

		public Companies ()
		{
		}
	}
}