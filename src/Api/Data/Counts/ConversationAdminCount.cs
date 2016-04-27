using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    [JsonConverter(typeof(ConversationAdminCountJsonClassConverter))]
    public class ConversationAdminCount
    {
        public List<AdminCount> admins  { set; get; }
    }
}