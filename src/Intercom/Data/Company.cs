﻿using System;
using Intercom.Core;
using System.Collections.Generic;
using Newtonsoft.Json;
using Intercom.Converters.AttributeConverters;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    public class Company : Model
    {
        public bool? remove { set; get; }
        public string name { get; set; }
        public Plan plan { get; set; }
        public string company_id { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? remote_created_at { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? created_at { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? updated_at { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? last_request_at { get; set; }
        public int? monthly_spend { get; set; }
        public int? session_count { get; set; }
        public int? user_count { get; set; }
        public int? size { get; set; }
        public string website { get; set; }
        public string industry { get; set; }
        public Dictionary<String, Object> custom_attributes { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<Tag> tags { get; set; }
    }
}
