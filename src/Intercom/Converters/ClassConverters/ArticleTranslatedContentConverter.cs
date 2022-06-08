using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Intercom.Converters.ClassConverters
{
    public class ArticleTranslatedContentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ArticleContent);
        }

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            JObject j = null;

            try
            {
                j = JObject.Load(reader);
                ArticleContent content = new ArticleContent();
                JObject result = new JObject();
                ArticleContents articleTranslations = new ArticleContents();

                
                foreach (JToken child in AllChildren(j.ToString()))
                {
                    string test = child.ToString();
                }

                content.locale = result.Root.ToString();
                content.title = result["title"].Value<string>();
                content.description = result["description"].Value<string>();
                content.body = result["body"].Value<string>();
                content.author_id = result["author_id"].Value<string>();
                content.state = result["state"].Value<string>();
                content.created_at = result["created_at"].Value<DateTimeOffset>();
                content.updated_at = result["updated_at"].Value<DateTimeOffset>();
                content.url = result["url"].Value<string>();
                content.type = result["type"].Value<string>();

                return content;
            }
            catch (Exception ex)
            {
                throw new JsonConverterException("Error while serializing ArticleTranslatedContentConverter endpoint json result.", ex)
                {
                    Json = j == null ? String.Empty : j.ToString(),
                    SerializationType = objectType.FullName
                };
            }
        }

        public override void WriteJson(JsonWriter writer,
                                       object value,
                                       JsonSerializer serializer)
        {
            String s = JsonConvert.SerializeObject(value,
                           Formatting.None,
                           new JsonSerializerSettings
                           {
                               NullValueHandling = NullValueHandling.Ignore
                           });

            writer.WriteRawValue(s);
        }

        public override bool CanRead
        {
            get { return true; }
        }
        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }
    }
}