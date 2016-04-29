using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;
using Newtonsoft.Json;
using Library.Converters.ClassConverters;

namespace Library.Data
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