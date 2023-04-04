using System;
using Intercom.Core;
using System.Collections.Generic;
using Newtonsoft.Json;
using Intercom.Converters.AttributeConverters;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    public class ArticleMultiLanguage : Model
    {
        public string workspace_id { get; set; }
        public string parent_id { get; set; }
        public string parent_type { get; set; }
        public string default_locale { get; set; } = "en";

        [JsonConverter(typeof(CustomTranslatedContentConverter))]
        public ArticleContents translated_content { get; set; }

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
        public Dictionary<String, Object> custom_attributes { get; set; }
    }


}
