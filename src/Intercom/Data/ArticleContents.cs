using System;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;
using Intercom.Core;
using System.Collections;
using Newtonsoft.Json;
using Intercom.Converters.AttributeConverters;
using Intercom.Converters.ClassConverters;
using Newtonsoft.Json.Linq;

namespace Intercom.Data
{
    public class CustomTranslatedContentConverter : JsonConverter<ArticleContents>
    {
        public override void WriteJson(JsonWriter writer, ArticleContents value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override ArticleContents ReadJson(JsonReader reader, Type objectType, ArticleContents existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            ArticleContents result = new ArticleContents();

            JObject obj = JObject.Load(reader);
            foreach (JToken childToken in obj.Children())
            {
                if (childToken.Path == "type")
                {
                    result.type = childToken.First.ToString();
                }
                else if (childToken.Path == "id")
                {
                    result.id = childToken.First.ToString();
                }
                else
                {
                    ArticleContent content = JsonConvert.DeserializeObject<ArticleContent>(childToken.First.ToString());
                    content.locale = childToken.Path;
                    result.data.Add(childToken.Path, content);
                }
            }

            return result;
        }
    }

    public class ArticleContents : Model
    {
        public Dictionary<string, ArticleContent> data { get; set; }

        public ArticleContents()
        {
            data = new Dictionary<string, ArticleContent>();
        }
        public IDictionaryEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public ArticleContent FindExact(string searchString)
        {
            ArticleContent result = new ArticleContent();

            //foreach (ArticleContent article in data)
            //{
            //    if (article.title == searchString)
            //    {
            //        result = article;
            //        break;
            //    }
            //    else if (!string.IsNullOrEmpty(article.description) && article.description.Contains(searchString))
            //    {
            //        result = article;
            //        break;
            //    }
            //    else
            //        result = null;
            //}

            return result;
        }

        public void RemoveDraftArticles()
        {
            //List<ArticleContent> articles = new List<ArticleContent>();

            //foreach (ArticleContent article in data)
            //{
            //    if (article.state == "draft")
            //    {
            //        articles.Add(article);
            //    }
            //}

            //foreach (ArticleContent remove in articles)
            //{
            //    data.Remove(remove);
            //}
        }

        public void RemovePublishedArticles()
        {
            //List<ArticleContent> articles = new List<ArticleContent>();

            //foreach (ArticleContent article in data)
            //{
            //    if (article.state == "published")
            //    {
            //        articles.Add(article);
            //    }
            //}

            //foreach (ArticleContent remove in articles)
            //{
            //    data.Remove(remove);
            //}
        }
    }
}
