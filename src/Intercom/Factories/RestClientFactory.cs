using System;
using RestSharp;
using RestSharp.Authenticators;
using Intercom.Core;

namespace Intercom.Factories
{
    public class RestClientFactory
    {

        protected const String INTERCOM_API_BASE_URL = "https://api.intercom.io/";

        private readonly Authentication _authentication;
        private readonly string _url;
        private IRestClient _restClient;

        public RestClientFactory(Authentication authentication)
        {
            _authentication = authentication;
            _url = INTERCOM_API_BASE_URL;
        }

        public RestClientFactory(Authentication authentication, string url)
        {
            _authentication = authentication;
            _url = url;
        }

        public virtual IRestClient RestClient
        {
            get
            {
                if (_restClient != null)
                {
                    return _restClient;
                }
                ConstructClient();
                return _restClient; 
            }
        }

        private void ConstructClient()
        {
            RestClient client = new RestClient(_url);

            if (!String.IsNullOrEmpty (_authentication.AppId) && !String.IsNullOrEmpty (_authentication.AppKey))
                client.Authenticator = new HttpBasicAuthenticator (_authentication.AppId, _authentication.AppKey);
            else
                client.Authenticator = new HttpBasicAuthenticator (_authentication.PersonalAccessToken, String.Empty);

            _restClient = client;
        }
    }
}