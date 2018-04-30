using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;
using Newtonsoft.Json;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    public class ConversationPart : Model
    {
        public string part_type { get; set; }
        public string body { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime created_at { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime updated_at { get; set; }
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime notified_at { get; set; }
        public Assignee assigned_to { get; set; }
        public Author author { get; set; }
        public List<Attachment> attachments { get; set; }
    }
}

