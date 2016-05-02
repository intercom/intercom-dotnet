using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;


namespace Intercom.Data
{
	public class Pages : Model
	{
		public string next { get; set; }
		public int page { get; set; }
		public int per_page { get; set; }
		public int total_pages { get; set; }
	}
}