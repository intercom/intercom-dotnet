﻿using System;
using Intercom.Core;
using System.Collections.Generic;
using Newtonsoft.Json;
using Intercom.Converters.AttributeConverters;

namespace Intercom.Data
{
	public class User : Model
	{
		public string user_id { get; set; }
		public string email { get; set; }
		public string phone { get; set; }
		public string name { get; set; }
		public int? updated_at { get; set; }
		public string last_seen_ip { get; set; }
		public bool? unsubscribed_from_emails { get; set; }
		public int? last_request_at { get; set; }
		public int? signed_up_at { get; set; }
		public int? created_at { get; set; }
		public int? session_count { get; set; }
		public string user_agent_data { get; set; }
		public object pseudonym { get; set; }
		public bool? anonymous { get; set; }
		public Dictionary<String, Object> custom_attributes { get; set; }
		public Avatar avatar { get; set; }
		public LocationData location_data { get; set; }
		[JsonConverter(typeof(ListJsonConverter))]
		public List<SocialProfile> social_profiles { get; set; }
		[JsonConverter(typeof(ListJsonConverter))]
		public List<Company> companies { get; set; }
		[JsonConverter(typeof(ListJsonConverter))]
		public List<Segments> segments { get; set; }
		[JsonConverter(typeof(ListJsonConverter))]
		public List<Tag> tags { get; set; }

		public User()
		{
		}
	}
}