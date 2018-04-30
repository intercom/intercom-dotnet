using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;
using Newtonsoft.Json;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
	public class Segment : Model
	{
        public string name { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime created_at { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime updated_at { get; set; }

		public Segment ()
		{
		}
	}
}

