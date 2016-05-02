using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;


namespace Intercom.Data
{
	public class LocationData
	{
		public string type { get; set; }
		public string city_name { get; set; }
		public string continent_code { get; set; }
		public string country_code { get; set; }
		public string country_name { get; set; }
		public double? latitude { get; set; }
		public double? longitude { get; set; }
		public object postal_code { get; set; }
		public string region_name { get; set; }
		public string timezone { get; set; }
	}
}

