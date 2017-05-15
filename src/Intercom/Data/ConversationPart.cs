using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections.Generic;

namespace Intercom.Data
{
    public class ConversationPart : Model
    {
        public string part_type { get; set; }
        public string body { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public long notified_at { get; set; }
        public object assigned_to { get; set; }
        public Author author { get; set; }
        public List<Attachment> attachments { get; set; }
    }
}

