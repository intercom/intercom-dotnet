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
    public class Conversation : Model
    {
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public Assignee assignee { get; set; }
        public User user { get; set; }
        public bool open { get; set; }
        public bool read { get; set; }
        public ConversationMessage conversation_message { get; set; }
        public List<ConversationPart> conversation_parts { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<Tags> tags { get; set; }
    }
}