using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections.Generic;

namespace Intercom.Data
{
	public class SocialProfiles
	{
		public string type { get; set; }
		public List<SocialProfile> social_profiles { get; set; }
	}
}

