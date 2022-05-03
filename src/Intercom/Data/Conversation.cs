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
        public DateTimeOffset? created_at { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset? updated_at { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public long? waiting_since { get; set; }
        public long? snoozed_until { get; set; }
        public Assignee assignee { get; set; }
        public ConversationUser user { get; set; }
        public bool open { get; set; }
        public bool read { get; set; }
        public string state { get; set; }
        public int total_count { get; set; }
        public ConversationMessage conversation_message { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<ConversationPart> conversation_parts { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<Tag> tags { get; set; }
        public ConversationRating conversation_rating { get; set; }
        public List<Customer> customers { get; set; }
    }
}
