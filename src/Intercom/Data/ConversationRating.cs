using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;

namespace Intercom.Data
{
    public class ConversationRating : Model
    {
        public string rating { get; set; }
        public string remark { get; set; }
        public long? created_at { get; set; }
        public Customer customer { get; set; }
        public Teammate teammate { get; set; }

        public ConversationRating()
        {
        }
    }
}
