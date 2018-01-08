using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

namespace Intercom.Data
{
	public class SocialProfile : Model
	{
		public override string type { get; set; }
		public string username { get; set; }
		public string url { get; set; }
	}
}

