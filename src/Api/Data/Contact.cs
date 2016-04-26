using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Library
{
    public class Contact : Model
    {
        public string user_id { get; set; }
        public bool? anonymous { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string pseudonym { get; set; }
        public Avatar avatar { get; set; }
        public string app_id { get; set; }
        public LocationData location_data { get; set; }
        public object last_request_at { get; set; }
        public object last_seen_ip { get; set; }
        public int? created_at { get; set; }
        public object remote_created_at { get; set; }
        public object signed_up_at { get; set; }
        public int? updated_at { get; set; }
        public int? session_count { get; set; }
        public bool? unsubscribed_from_emails { get; set; }
        public object user_agent_data { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<Tag> tags { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<Segment> segments { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<Company> companies { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<SocialProfile> social_profiles { get; set; }
        public Dictionary<String, String> custom_attributes { get; set; }
    }
}