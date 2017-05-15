using System;
using Newtonsoft.Json;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;


namespace Intercom.Data
{
	[JsonConverter(typeof(PlanJsonConverter))]
	public class Plan : Model
	{
		public string name { get; set; }

		public override string ToString()
		{
			return this.name;
		}
	}
}

