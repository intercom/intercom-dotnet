using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using Newtonsoft.Json;
using Library.Converters.ClassConverters;

namespace Library.Data
{
    
    [JsonConverter(typeof(ConversationAppCountJsonConverter))]
    public class ConversationAppCount
    {
        public int closed { set; get; }
        public int open { set; get; }
        public int assigned { set; get; }
        public int unassigned { set; get; }

        public ConversationAppCount()
        {
        }
    }
}