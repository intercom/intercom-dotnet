using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using Newtonsoft.Json;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    public class Note : Model
    {
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? created_at { get; set; }
        public string body { get; set; }
        public Admin author { get; set; }
        public User user { get; set; }

        public Note()
        {
        }
    }
}
