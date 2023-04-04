using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;
using Intercom.Converters.ClassConverters;
using Newtonsoft.Json;

namespace Intercom.Data
{
    public class ConversationPart : Model
    {
        public string part_type { get; set; }
        public string body { get; set; }
        public long created_at { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset? updated_at { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset? notified_at { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public Assignee assigned_to { get; set; }
        public Author author { get; set; }
        public List<Attachment> attachments { get; set; }
    }
}

