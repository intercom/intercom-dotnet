using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using Intercom.Converters.ClassConverters;
using Newtonsoft.Json;

namespace Intercom.Data
{
    public class Note : Model
    {
        public DateTimeOffset? created_at { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public string body { get; set; }
        public Admin author { get; set; }
        public User user { get; set; }

        public Note()
        {
        }
    }
}
