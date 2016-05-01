using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using System.Collections.Generic;
using Newtonsoft.Json;
using Library.Converters.AttributeConverters;

namespace Library.Data
{
	public class Event : Model
	{
		public string event_name { get; set; }
		public long? created_at { get; set; }
		public string user_id { get; set; }
		public string email { get; set; }

		[JsonConverter(typeof(MetadataJsonConverter))]
		public Metadata metadata { get; set; }

		public Event()
		{
		}
	}
}