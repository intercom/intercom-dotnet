using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Core;
using Intercom.Data;
using Intercom.Factories;
using Intercom.Converters;
using Newtonsoft.Json;

namespace Intercom.Clients
{
    [Obsolete("Articles Object Model can only be used with Intercom API v2.0 or greater.")]
    public class ArticlesClient : Client
    {
        private const String ARTICLES_RESOURCE = "articles";

        public ArticlesClient(RestClientFactory restClientFactory)
            : base(ARTICLES_RESOURCE, restClientFactory)
        {
        }

        [Obsolete("This constructor is deprecated as of 3.0.0 and will soon be removed, please use CompanyClient(RestClientFactory restClientFactory)")]
        public ArticlesClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, ARTICLES_RESOURCE, authentication)
        {
        }

        [Obsolete("This constructor is deprecated as of 3.0.0 and will soon be removed, please use CompanyClient(RestClientFactory restClientFactory)")]
        public ArticlesClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, ARTICLES_RESOURCE, authentication)
        {
        }

        public Article Create(Article article)
        {
            return CreateOrUpdate(article);
        }

        public Article Update(Article article)
        {
            return CreateOrUpdate(article);
        }

        private Article CreateOrUpdate(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            if (article.custom_attributes != null && article.custom_attributes.Any())
            {
                if (article.custom_attributes.Count > 100)
                    throw new ArgumentException("Maximum of 100 fields.");

                foreach (var attr in article.custom_attributes)
                {
                    if (attr.Key.Contains(".") || attr.Key.Contains("$"))
                        throw new ArgumentException(String.Format("Field names must not contain Periods (.) or Dollar ($) characters. key: {0}", attr.Key));

                    if (attr.Key.Length > 190)
                        throw new ArgumentException(String.Format("Field names must be no longer than 190 characters. key: {0}", attr.Key));

                    if (attr.Value == null)
                        throw new ArgumentException(String.Format("'value' is null. key: {0}", attr.Key));
                }
            }

            ClientResponse<Article> result = null;
            result = Post<Article>(Transform(article));
            return result.Result;
        }

        public Article View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }
            ClientResponse<Article> result = null;
            result = Get<Article>(resource: ARTICLES_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;
        }

        public Articles GetArticles(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<Articles> result = null;
            parameters.Add("per_page", "150");

            if (!String.IsNullOrEmpty(article.id))
            {
                result = Get<Articles>(resource: ARTICLES_RESOURCE + Path.DirectorySeparatorChar + article.id);
            }
            else if (!String.IsNullOrEmpty(article.title))
            {
                parameters.Add(Constants.TITLE, article.title);
                result = Get<Articles>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(article.article_id))
            {
                parameters.Add(Constants.ARTICLE_ID, article.article_id);
                result = Get<Articles>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(article.state))
            {
                result = Get<Articles>(parameters: parameters);
            }
            else
            {
                throw new ArgumentException("you need to provide either 'article.id', 'article.article_id' to view an article.");
            }

            return result.Result;
        }

        public ArticlesMultiLanguage GetArticlesML(ArticleMultiLanguage article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<ArticlesMultiLanguage> result = null;
            parameters.Add("per_page", "150");

            if (!String.IsNullOrEmpty(article.id))
            {
                result = Get<ArticlesMultiLanguage>(resource: ARTICLES_RESOURCE + Path.DirectorySeparatorChar + article.id);
            }
            else if (!String.IsNullOrEmpty(article.title))
            {
                parameters.Add(Constants.TITLE, article.title);
                result = Get<ArticlesMultiLanguage>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(article.state))
            {
                result = Get<ArticlesMultiLanguage>(parameters: parameters);
            }
            else
            {
                throw new ArgumentException("you need to provide either 'article.id', to view an article.");
            }

            return result.Result;
        }

        public Article View(Article article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<Article> result = null;

            if (!String.IsNullOrEmpty(article.id))
            {
                result = Get<Article>(resource: ARTICLES_RESOURCE + Path.DirectorySeparatorChar + article.id);
            }
            else if (!String.IsNullOrEmpty(article.title))
            {
                parameters.Add(Constants.TITLE, article.title);
                result = Get<Article>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(article.article_id))
            {
                parameters.Add(Constants.ARTICLE_ID, article.article_id);
                result = Get<Article>(parameters: parameters);
            }
            else
            {
                throw new ArgumentException("you need to provide either 'article.id', 'article.article_id' to view an article.");
            }
            return result.Result;
        }
        public ArticleMultiLanguage ViewML(ArticleMultiLanguage article)
        {
            if (article == null)
            {
                throw new ArgumentNullException(nameof(article));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<ArticleMultiLanguage> result = null;

            if (!String.IsNullOrEmpty(article.id))
            {
                result = Get<ArticleMultiLanguage>(resource: ARTICLES_RESOURCE + Path.DirectorySeparatorChar + article.id);
            }
            else if (!String.IsNullOrEmpty(article.title))
            {
                parameters.Add(Constants.TITLE, article.title);
                result = Get<ArticleMultiLanguage>(parameters: parameters);
            }
            else
            {
                throw new ArgumentException("you need to provide either 'article.id',  to view an article.");
            }
            return result.Result;
        }
        public Articles List()
        {
            ClientResponse<Articles> result = null;
            result = Get<Articles>();
            return result.Result;
        }

        public Articles List(Dictionary<String, String> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!parameters.Any())
            {
                throw new ArgumentException("'parameters' argument is empty.");
            }

            ClientResponse<Articles> result = null;
            result = Get<Articles>(parameters: parameters);
            return result.Result;
        }

        public Articles Scroll(String scrollParam = null)
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>();
            ClientResponse<Articles> result = null;

            if (!String.IsNullOrWhiteSpace(scrollParam))
            {
                parameters.Add("scroll_param", scrollParam);
            }

            result = Get<Articles>(parameters: parameters, resource: ARTICLES_RESOURCE + Path.DirectorySeparatorChar + "scroll");
            return result.Result;
        }

        private String Transform(Article article)
        {
            var body = new
            {
                created_at = article.created_at,
                article_id = article.article_id,
                title = article.title,
                description = article.description,
                custom_attributes = article.custom_attributes,
                article_body = article.body,
                author_id = article.author_id,
                state = article.state,
                updated_at= article.updated_at,
                url = article.url
            };

            return JsonConvert.SerializeObject(body,
                           Formatting.None,
                           new JsonSerializerSettings
                           {
                               NullValueHandling = NullValueHandling.Ignore
                           });
        }
    }
}