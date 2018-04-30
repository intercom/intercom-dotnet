using System;
using System.Collections.Generic;
using Intercom.Clients;
using Intercom.Converters.AttributeConverters;
using Intercom.Converters.ClassConverters;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Newtonsoft.Json;

namespace Intercom.Data
{
    public class Conversation : Model
    {
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime created_at { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime updated_at { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? waiting_since { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? snoozed_until { get; set; }
        public Assignee assignee { get; set; }
        public User user { get; set; }
        public bool open { get; set; }
        public bool read { get; set; }
        public string state { get; set; }
        public int total_count { get; set; }
        public ConversationMessage conversation_message { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<ConversationPart> conversation_parts { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<Tag> tags { get; set; }
        public List<Customer> customers { get; set; }
    }
}
