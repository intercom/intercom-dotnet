using System;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;
using Intercom.Core;
using System.Collections;
using Newtonsoft.Json;
using Intercom.Converters.AttributeConverters;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    public class ArticlesMultiLanguage : Models
    {
        public List<ArticleMultiLanguage> data {get; set;}

        public ArticlesMultiLanguage()
        {
        }
        public IEnumerator<ArticleMultiLanguage> GetEnumerator()
        {
            return data.GetEnumerator();
        }
        public ArticleMultiLanguage FindExact(string searchString)
        {
            ArticleMultiLanguage result = new ArticleMultiLanguage();

            foreach (ArticleMultiLanguage article in data)
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
        public void Search(string searchString)
        {
            List<ArticleMultiLanguage> emptyArticles = new List<ArticleMultiLanguage>();
            foreach (ArticleMultiLanguage article in data)
            {
                List<string> toRemove = new List<string>();
                foreach (KeyValuePair<string, ArticleContent> localised in article.translated_content.data)
                {
                    string feTitle = localised.Value.title ?? "";
                    string feDescription = localised.Value.description ?? "";
                    string feBody = localised.Value.body ?? "";

                    if (!feTitle.Contains(searchString) && !feDescription.Contains(searchString) && !feBody.Contains(searchString))
                    {
                        toRemove.Add(localised.Key);
                    }
                }
                foreach (string key in toRemove)
                {
                    article.translated_content.data.Remove(key);
                }
                if (article.translated_content.data.Count == 0)
                {
                    emptyArticles.Add(article);
                }
            }
            foreach (ArticleMultiLanguage emptyArticle in emptyArticles)
            {
                data.Remove(emptyArticle);
            }
        }

        public void NewOrModifiedArticles(DateTime marker)
        {
            List<ArticleMultiLanguage> emptyArticles = new List<ArticleMultiLanguage>();
            foreach (ArticleMultiLanguage article in data)
            {
                List<string> toRemove = new List<string>();
                foreach(KeyValuePair<string, ArticleContent> localised in article.translated_content.data)
                {
                    if (localised.Value.created_at < marker || localised.Value.updated_at < marker)
                    {
                        toRemove.Add(localised.Key);
                    }
                }
                foreach (string key in toRemove)
                {
                    article.translated_content.data.Remove(key);
                }
                if (article.translated_content.data.Count == 0)
                {
                    emptyArticles.Add(article);
                }
            }
            foreach (ArticleMultiLanguage emptyArticle in emptyArticles)
            {
                data.Remove(emptyArticle);
            }
        }

        public void SingleLocaleArticles(string locale)
        {
            List<ArticleMultiLanguage> emptyArticles = new List<ArticleMultiLanguage>();
            foreach (ArticleMultiLanguage article in data)
            {
                List<string> toRemove = new List<string>();
                foreach (KeyValuePair<string, ArticleContent> localised in article.translated_content.data)
                {
                    if (localised.Value.locale != locale && localised.Value.locale != null)
                    {
                        toRemove.Add(localised.Key);
                    }
                }
                foreach (string key in toRemove)
                {
                    article.translated_content.data.Remove(key);
                }
                if (article.translated_content.data.Count == 0)
                {
                    emptyArticles.Add(article);
                }
            }
            foreach (ArticleMultiLanguage emptyArticle in emptyArticles)
            {
                data.Remove(emptyArticle);
            }
        }

        public void RemoveDraftArticles()
        {
            List<ArticleMultiLanguage> articles = new List<ArticleMultiLanguage>();

            List<ArticleMultiLanguage> emptyArticles = new List<ArticleMultiLanguage>();
            foreach (ArticleMultiLanguage article in data)
            {
                List<string> toRemove = new List<string>();
                foreach (KeyValuePair<string, ArticleContent> localised in article.translated_content.data)
                {
                    if (localised.Value.state == "draft")
                    {
                        toRemove.Add(localised.Key);
                    }
                }
                foreach (string key in toRemove)
                {
                    article.translated_content.data.Remove(key);
                }
                if (article.translated_content.data.Count == 0)
                {
                    emptyArticles.Add(article);
                }
            }
            foreach (ArticleMultiLanguage emptyArticle in emptyArticles)
            {
                data.Remove(emptyArticle);
            }
        }

        public void RemovePublishedArticles()
        {
            List<ArticleMultiLanguage> articles = new List<ArticleMultiLanguage>();

            List<ArticleMultiLanguage> emptyArticles = new List<ArticleMultiLanguage>();
            foreach (ArticleMultiLanguage article in data)
            {
                List<string> toRemove = new List<string>();
                foreach (KeyValuePair<string, ArticleContent> localised in article.translated_content.data)
                {
                    if (localised.Value.state == "published")
                    {
                        toRemove.Add(localised.Key);
                    }
                }
                foreach (string key in toRemove)
                {
                    article.translated_content.data.Remove(key);
                }
                if (article.translated_content.data.Count == 0)
                {
                    emptyArticles.Add(article);
                }
            }
            foreach (ArticleMultiLanguage emptyArticle in emptyArticles)
            {
                data.Remove(emptyArticle);
            }
        }
    }
}
