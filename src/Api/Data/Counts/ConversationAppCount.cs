using System;
using Newtonsoft.Json;

namespace Library
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