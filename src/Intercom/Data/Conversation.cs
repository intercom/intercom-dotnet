using System;
using System.Collections.Generic;
using Intercom.Clients;
using Intercom.Converters.AttributeConverters;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Newtonsoft.Json;

namespace Intercom.Data
{
    public class Conversation : Model
    {
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public Assignee assignee { get; set; }
        public User user { get; set; }
        public bool open { get; set; }
        public bool read { get; set; }
        public ConversationMessage conversation_message { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<ConversationPart> conversation_parts { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<Tag> tags { get; set; }
    }
}
