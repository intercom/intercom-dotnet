using System;
using Intercom.Core;
using System.Collections.Generic;
using Newtonsoft.Json;
using Intercom.Converters.AttributeConverters;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    public class ArticleContent : Model
    {
        public string locale { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string body { get; set; }
        public string author_id { get; set; }
        public string state { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))]
        public DateTimeOffset? created_at { get; set; }
        [JsonConverter(typeof(DateTimeOffsetJsonConverter))] 
        public DateTimeOffset? updated_at { get; set; }
        public string url { get; set; }

    }
}
