using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Converters.ClassConverters;
using Newtonsoft.Json;


using Intercom.Clients;

using Intercom.Exceptions;


namespace Intercom.Data
{
    public class Segment : Model
    {
        public string name { get; set; }
        public DateTimeOffset? created_at { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset? updated_at { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public int? count { get; set; }

        public Segment()
        {
        }
    }
}

