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
    [JsonConverter(typeof(ConversationAdminCountJsonConverter))]
    public class ConversationAdminCount
    {
        public class AdminCount
        {
            public string id { set; get; }

            public String name { set; get; }

            public int open { set; get; }

            public int closed { set; get ; }
        }

        public List<AdminCount> admins  { set; get; }
    }
}