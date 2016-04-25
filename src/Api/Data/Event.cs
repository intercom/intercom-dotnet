using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
	public class Event : Model
	{
		public string event_name { get; set; }
		public int created_at { get; set; }
		public string user_id { get; set; }
		public string email { get; set; }

		[JsonConverter(typeof(MetadataJsonConverter))]
		public Metadata metadata { get; set; }

		public Event()
		{
		}
	}
}