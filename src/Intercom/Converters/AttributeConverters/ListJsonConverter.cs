using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Intercom.Converters.AttributeConverters
{
    public class ListJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(User);
        }

        public override object ReadJson(JsonReader reader, 
                                        Type objectType, 
                                        object existingValue, 
                                        JsonSerializer serializer)
        {
            JObject jobject = null;

            try
            {
                jobject = JObject.Load(reader);
                Object result = null;

                if (objectType == typeof(List<Company>))
                    result = GetList<Company>(jobject, "companies");
                else if (objectType == typeof(List<SocialProfile>))
                    result = GetList<SocialProfile>(jobject, "social_profiles");
                else if (objectType == typeof(List<Tag>))
                    result = GetList<Tag>(jobject, "tags");
                else if (objectType == typeof(List<Segment>))
                    result = GetList<Segment>(jobject, "segments");
                else if (objectType == typeof(List<ConversationPart>))
                    result = GetList<ConversationPart>(jobject, "conversation_parts");
                else if (objectType == typeof(List<Article>))
                    result = GetList<Article>(jobject, "articles");
                else if (objectType == typeof(List<ArticleMultiLanguage>))
                    result = GetList<Article>(jobject, "articles");
                else if (objectType == typeof(List<ArticleContent>))
                    result = GetListArticlesML(jobject, "articles_content");
                else if (objectType == typeof(List<Contact>))
                    result = GetList<Contact>(jobject, "contacts");

                return result;
            }
            catch (Exception ex)
            {
                throw new JsonConverterException("Error while serializing AppCount endpoint json result.", ex)
                { 
                    Json = jobject == null ? String.Empty : jobject.ToString(),
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

        private List<T> GetList<T>(JObject jobject, String key)
			where T: class
        {
            var value = jobject.GetValue(key);
            var result = (JsonConvert.DeserializeObject(value.ToString(), typeof(T[])) as T[]).ToList();
            return result;
        }

        private List<ArticleContent> GetListArticlesML(JObject jobject, String key)
        {
            List<ArticleContent> result = new List<ArticleContent>();

            foreach (JObject articleml in jobject["children"])
            {
                ArticleContent resultArticle = new ArticleContent();
                resultArticle.locale = articleml["root"].ToString();
                resultArticle.title = articleml["title"].ToString();
                resultArticle.description = articleml["description"].ToString();
                resultArticle.body = articleml["body"].ToString();
                resultArticle.author_id = articleml["author_id"].ToString();
                resultArticle.state = articleml["state"].ToString();
                resultArticle.created_at = articleml["created_at"].ToObject<DateTimeOffset>();
                resultArticle.updated_at = articleml["updated_at"].ToObject<DateTimeOffset>();
                resultArticle.url = articleml["url"].ToString();
                resultArticle.type = articleml["article_content"].ToString();

                result.Add(resultArticle);
            }
            return result;
        }
    }
}