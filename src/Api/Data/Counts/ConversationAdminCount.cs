using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    [JsonConverter(typeof(ConversationAdminCountJsonClassConverter))]
    public class ConversationAdminCount
    {
        public class AdminCount
        {
            public string id { set; get; }

            public String name { set; get; }

            public String open { set; get; }

            public String closed { set; get ; }
        }

        public List<AdminCount> admins  { set; get; }
    }
}