using System;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;
using Intercom.Core;
using System.Collections;
using Newtonsoft.Json;
using Intercom.Converters.AttributeConverters;

namespace Intercom.Data
{
    public class Articles : Models
    {
        public List<Article> data { set; get; }

        public string scroll_param { get; set; }

        public Articles ()
        {
        }
        public IEnumerator<Article> GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public Article FindExact(string searchString)
        {
            Article result = new Article();

            foreach (Article article in data)
            {
                if (article.title == searchString)
                {
                    result = article;
                    break;
                }
                else if (!string.IsNullOrEmpty(article.description) && article.description.Contains(searchString))
                {
                    result = article;
                    break;
                }
                else
                    result = null;
            }

            return result;
        }

        public void RemoveDraftArticles()
        {
            List<Article> articles = new List<Article>();

            foreach (Article article in data)
            {
                if (article.state == "draft")
                {
                    articles.Add(article);
                }
            }

            foreach (Article remove in articles)
            {
                data.Remove(remove);
            }
        }

        public void RemovePublishedArticles()
        {
            List<Article> articles = new List<Article>();

            foreach (Article article in data)
            {
                if (article.state == "published")
                {
                    articles.Add(article);
                }
            }

            foreach (Article remove in articles)
            {
                data.Remove(remove);
            }
        }
    }
}
