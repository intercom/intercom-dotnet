using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using Library.Clients;
using Library.Core;
using Library.Data;
using Library.Exceptions;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace Library.Core
{
    public class Client
    {
        protected const String INTERCOM_API_BASE_URL = "https://api.intercom.io/";
        protected const String CONTENT_TYPE_HEADER = "Content-Type";
        protected const String CONTENT_TYPE_VALUE = "application/json";
        protected const String ACCEPT_HEADER = "Accept";
        protected const String ACCEPT_VALUE = "application/json";
        protected const String ACCEPT_CHARSET_HEADER = "Accept-Charset";
        protected const String ACCEPT_CHARSET_VALUE = "UTF-8";

        protected readonly String URL;
        protected readonly String RESRC;
        protected readonly Authentication AUTH;

        public Client(String url, String resource, Authentication authentication)
        {
            if (authentication == null)
                throw new ArgumentNullException("'basicAuthentication' argument is null.");
                
            if (String.IsNullOrEmpty(url))
                throw new ArgumentNullException("'baseUrl' argument is null.");

            if (String.IsNullOrEmpty(resource))
                throw new ArgumentNullException("'resource' argument is null.");

            this.URL = url;
            this.RESRC = resource;
            this.AUTH = authentication;
        }

        protected virtual ClientResponse<T> Get<T>(
            Dictionary<String, String> headers = null,
            Dictionary<String, String> parameters = null,
            String resource = null)
			where T : class
        {
            IRestClient client = BuildClient();
            IRestRequest request = BuildRequest(httpMethod: Method.GET, headers: headers, parameters: parameters, resource: resource);
            IRestResponse response = client.Execute(request);
            return HandleResponse<T>(response);
        }

        protected virtual ClientResponse<T> Post<T>(String body, 
                                                    Dictionary<String, String> headers = null, 
                                                    Dictionary<String, String> parameters = null,
                                                    String resource = null)
			where T : class
        {
            if (String.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException("'body' argument is null.");
            }

            IRestClient client = BuildClient();
            IRestRequest request = BuildRequest(httpMethod: Method.POST, headers: headers, parameters: parameters, body: body, resource: resource);
            IRestResponse response = client.Execute(request);
            return HandleResponse <T>(response);
        }

        protected virtual ClientResponse<T> Post<T>(T body, 
                                                    Dictionary<String, String> headers = null, 
                                                    Dictionary<String, String> parameters = null,
                                                    String resource = null)
			where T : class
        {
            if (body == null)
            {
                throw new ArgumentNullException("'body' argument is null.");
            }

            String requestBody = Serialize<T>(body);
            IRestClient client = BuildClient();
            IRestRequest request = BuildRequest(httpMethod: Method.POST, headers: headers, parameters: parameters, body: requestBody, resource: resource);
            IRestResponse response = client.Execute(request);
            return HandleResponse <T>(response);
        }

        protected virtual ClientResponse<T> Put<T>(String body, 
                                                   Dictionary<String, String> headers = null, 
                                                   Dictionary<String, String> parameters = null,
                                                   String resource = null)
            where T : class
        {
            if (String.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException("'body' argument is null.");
            }

            IRestClient client = BuildClient();
            IRestRequest request = BuildRequest(httpMethod: Method.PUT, headers: headers, parameters: parameters, body: body, resource: resource);
            IRestResponse response = client.Execute(request);
            return HandleResponse <T>(response);
        }

        protected virtual ClientResponse<T> Put<T>(T body, 
                                                   Dictionary<String, String> headers = null, 
                                                   Dictionary<String, String> parameters = null,
                                                   String resource = null)
            where T : class
        {
            if (body == null)
            {
                throw new ArgumentNullException("'body' argument is null.");
            }

            String requestBody = Serialize<T>(body);
            IRestClient client = BuildClient();
            IRestRequest request = BuildRequest(httpMethod: Method.PUT, headers: headers, parameters: parameters, body: requestBody, resource: resource);
            IRestResponse response = client.Execute(request);
            return HandleResponse <T>(response);
        }

        protected virtual ClientResponse<T>  Delete<T>(
            Dictionary<String, String> headers = null, 
            Dictionary<String, String> parameters = null,
            String resource = null)
			where T : class
        {
            IRestClient client = BuildClient();
            IRestRequest request = BuildRequest(httpMethod: Method.DELETE, headers: headers, parameters: parameters, resource: resource);
            IRestResponse response = client.Execute(request);
            return HandleResponse<T>(response);
        }

        protected virtual IRestRequest BuildRequest(Method httpMethod = Method.GET,
                                                    Dictionary<String, String> headers = null, 
                                                    Dictionary<String, String> parameters = null, 
                                                    String body = null,
                                                    String resource = null)
        {
            String final = String.IsNullOrEmpty(resource) ? RESRC : resource;

            IRestRequest request = new RestRequest(final, httpMethod);
            request.AddHeader(CONTENT_TYPE_HEADER, CONTENT_TYPE_VALUE);
            request.AddHeader(ACCEPT_CHARSET_HEADER, ACCEPT_CHARSET_VALUE);
            request.AddHeader(ACCEPT_HEADER, ACCEPT_VALUE);

            if (headers != null && headers.Any())
                AddHeaders(request, headers);

            if (parameters != null && parameters.Any())
                AddParameters(request, parameters);

            if (!String.IsNullOrEmpty(body))
                AddBody(request, body);

            return request;
        }

        protected virtual IRestClient BuildClient()
        {
            RestClient client = new RestClient(URL);
            client.Authenticator = new HttpBasicAuthenticator(AUTH.AppId, AUTH.AppKey);
            return client;
        }

        protected virtual void AddHeaders(IRestRequest request, 
                                          Dictionary<String, String> headers)
        {
            if (request == null)
                throw new ArgumentNullException("'request' argument is null.");

            if (headers == null)
                throw new ArgumentNullException("'headers' argument is null.");

            foreach (var header in headers)
                request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
        }

        protected virtual void AddParameters(IRestRequest request, 
                                             Dictionary<String, String> parameters)
        {
            if (request == null)
                throw new ArgumentNullException("'request' argument is null.");

            if (parameters == null)
                throw new ArgumentNullException("'headers' argument is null.");

            foreach (var parameter in parameters)
                request.AddParameter(parameter.Key, parameter.Value, ParameterType.QueryString);
        }

        protected virtual void AddBody(IRestRequest request, String body)
        {
            if (request == null)
                throw new ArgumentNullException("'request' argument is null.");

            if (!String.IsNullOrEmpty(body))
                request.AddParameter("application/json", body, ParameterType.RequestBody);
        }

        protected virtual ClientResponse<T> HandleResponse<T>(IRestResponse response)
			where T: class
        {
            ClientResponse<T> clientResponse = null;
            int statusCode = (int)response.StatusCode;

            if (statusCode >= 200 && statusCode < 300)
            {
                clientResponse = HandleNormalResponse <T>(response) as ClientResponse<T>;
            }
            else
            {
                clientResponse = HandleErrorResponse <T>(response) as ClientResponse<T>;
            }	

            AssertIfAnyErrors(clientResponse);

            return clientResponse;
        }

        protected T Deserialize<T>(String data)
            where T : class
        {
            return JsonConvert.DeserializeObject(data, typeof(T)) as T;
        }

        protected String Serialize<T>(T data)
            where T : class
        {
            return JsonConvert.SerializeObject(data,
                typeof(T),
                Formatting.None, 
                new JsonSerializerSettings
                { 
                    NullValueHandling = NullValueHandling.Ignore
                });
        }

        protected ClientResponse<T> HandleErrorResponse<T>(IRestResponse response)
			where T : class
        {
            if (String.IsNullOrEmpty(response.Content))
            {
                return new ClientResponse<T>(response: response);
            }
            else
            {
                Errors errors = Deserialize<Errors>(response.Content);
                return new ClientResponse<T>(response: response, errors: errors);
            }
        }

        protected ClientResponse<T> HandleNormalResponse<T>(IRestResponse response)
			where T : class
        {
            return new ClientResponse<T>(response: response, result: Deserialize<T>(response.Content));
        }

        protected void AssertIfAnyErrors<T>(ClientResponse<T> response)
			where T : class
        {
            if (response.Errors != null && response.Errors.errors != null && response.Errors.errors.Any())
            {
                throw new EndpointException((int)response.Response.StatusCode, 
                    response.Response.StatusDescription,
                    response.Errors,
                    response.Response.Content);
            }
        }
    }
}