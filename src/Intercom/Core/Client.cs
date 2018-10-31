using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Intercom.Factories;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;

namespace Intercom.Core
{
    public class Client
    {
        protected const String CONTENT_TYPE_HEADER = "Content-Type";
        protected const String CONTENT_TYPE_VALUE = "application/json";
        protected const String ACCEPT_HEADER = "Accept";
        protected const String ACCEPT_VALUE = "application/json";
        protected const String ACCEPT_CHARSET_HEADER = "Accept-Charset";
        protected const String ACCEPT_CHARSET_VALUE = "UTF-8";
        protected const String USER_AGENT_HEADER = "User-Agent";
        protected const String USER_AGENT_VALUE = "intercom-dotnet/2.0.0";

        protected readonly String RESRC;
        protected readonly RestClientFactory _restClientFactory;

        public Client(String resource, RestClientFactory restClientFactory)
        {
            if (restClientFactory == null)
                throw new ArgumentNullException(nameof(restClientFactory));

            if (String.IsNullOrEmpty(resource))
                throw new ArgumentNullException(nameof(resource));

            RESRC = resource;
            _restClientFactory = restClientFactory;
        }

        protected virtual ClientResponse<T> Get<T>(
            Dictionary<String, String> headers = null,
            Dictionary<String, String> parameters = null,
            String resource = null)
			where T : class
        {
            ClientResponse<T> clientResponse = null;

            IRestClient client = null;
            try
            {
                client = BuildClient();
                IRestRequest request = BuildRequest(httpMethod: Method.GET, headers: headers, parameters: parameters, resource: resource);
                IRestResponse response = client.Execute(request);
                clientResponse = HandleResponse<T>(response);
            }
            catch(ApiException ex)
            {
                throw ex;
            }
            catch (JsonConverterException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new IntercomException(String.Format("An exception occurred " +
                        "while calling the endpoint. Method: {0}, Url: {1}, Resource: {2}, Sub-Resource: {3}",
                        "GET", client.BaseUrl, RESRC, resource), ex); 
            }

            return clientResponse;
        }

        protected virtual ClientResponse<T> Post<T>(String body, 
                                                    Dictionary<String, String> headers = null, 
                                                    Dictionary<String, String> parameters = null,
                                                    String resource = null)
			where T : class
        {
            if (String.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException(nameof(body));
            }

            ClientResponse<T> clientResponse = null;

            IRestClient client = null;
            try
            {
                client = BuildClient();
                IRestRequest request = BuildRequest(httpMethod: Method.POST, headers: headers, parameters: parameters, body: body, resource: resource);
                IRestResponse response = client.Execute(request);
                clientResponse = HandleResponse <T>(response);
            }
            catch(ApiException ex)
            {
                throw ex;
            }
            catch (JsonConverterException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new IntercomException(String.Format("An exception occurred " +
                        "while calling the endpoint. Method: {0}, Url: {1}, Resource: {2}, Sub-Resource: {3}, Body: {4}",
                        "POST", client.BaseUrl, RESRC, resource, body), ex); 
            }

            return clientResponse;
        }

        protected virtual ClientResponse<T> Post<T>(T body, 
                                                    Dictionary<String, String> headers = null, 
                                                    Dictionary<String, String> parameters = null,
                                                    String resource = null)
			where T : class
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            ClientResponse<T> clientResponse = null;

            IRestClient client = null;
            try
            {
                String requestBody = Serialize<T>(body);
                client = BuildClient();
                IRestRequest request = BuildRequest(httpMethod: Method.POST, headers: headers, parameters: parameters, body: requestBody, resource: resource);
                IRestResponse response = client.Execute(request);
                clientResponse = HandleResponse <T>(response);
            }
            catch(ApiException ex)
            {
                throw ex;
            }
            catch (JsonConverterException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new IntercomException(String.Format("An exception occurred " +
                        "while calling the endpoint. Method: {0}, Url: {1}, Resource: {2}, Sub-Resource: {3}, Body-Type: {4}",
                        "POST", client.BaseUrl, RESRC, resource, typeof(T)), ex); 
            }

            return clientResponse;
        }

        protected virtual ClientResponse<T> Put<T>(String body, 
                                                   Dictionary<String, String> headers = null, 
                                                   Dictionary<String, String> parameters = null,
                                                   String resource = null)
            where T : class
        {
            if (String.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException(nameof(body));
            }

            ClientResponse<T> clientResponse = null;

            IRestClient client = null;
            try
            {
                client = BuildClient();
                IRestRequest request = BuildRequest(httpMethod: Method.PUT, headers: headers, parameters: parameters, body: body, resource: resource);
                IRestResponse response = client.Execute(request);
                clientResponse = HandleResponse <T>(response);
            }
            catch(ApiException ex)
            {
                throw ex;
            }
            catch (JsonConverterException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new IntercomException(String.Format("An exception occurred " +
                        "while calling the endpoint. Method: {0}, Url: {1}, Resource: {2}, Sub-Resource: {3}, Body: {4}",
                        "POST", client.BaseUrl, RESRC, resource, body), ex); 
            }

            return clientResponse;
        }

        protected virtual ClientResponse<T> Put<T>(T body, 
                                                   Dictionary<String, String> headers = null, 
                                                   Dictionary<String, String> parameters = null,
                                                   String resource = null)
            where T : class
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            ClientResponse<T> clientResponse = null;

            IRestClient client = null;
            try
            {
                String requestBody = Serialize<T>(body);
                client = BuildClient();
                IRestRequest request = BuildRequest(httpMethod: Method.PUT, headers: headers, parameters: parameters, body: requestBody, resource: resource);
                IRestResponse response = client.Execute(request);
                clientResponse = HandleResponse <T>(response);
            }
            catch(ApiException ex)
            {
                throw ex;
            }
            catch (JsonConverterException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new IntercomException(String.Format("An exception occurred " +
                        "while calling the endpoint. Method: {0}, Url: {1}, Resource: {2}, Sub-Resource: {3}",
                        "POST", client.BaseUrl, RESRC, resource), ex); 
            }

            return clientResponse;
        }

        protected virtual ClientResponse<T>  Delete<T>(
            Dictionary<String, String> headers = null, 
            Dictionary<String, String> parameters = null,
            String resource = null)
			where T : class
        {
            ClientResponse<T> clientResponse = null;

            IRestClient client = null;
            try
            {
                client = BuildClient();
                IRestRequest request = BuildRequest(httpMethod: Method.DELETE, headers: headers, parameters: parameters, resource: resource);
                IRestResponse response = client.Execute(request);
                clientResponse = HandleResponse<T>(response);
            }
            catch(ApiException ex)
            {
                throw ex;
            }
            catch (JsonConverterException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new IntercomException(String.Format("An exception occurred " +
                        "while calling the endpoint. Method: {0}, Url: {1}, Resource: {2}, Sub-Resource: {3}",
                        "POST", client.BaseUrl, RESRC, resource), ex); 
            }
        
            return clientResponse;
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
            request.AddHeader(USER_AGENT_HEADER, USER_AGENT_VALUE);

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
            return _restClientFactory.RestClient;
        }

        protected virtual void AddHeaders(IRestRequest request, 
                                          Dictionary<String, String> headers)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            foreach (var header in headers)
                request.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
        }

        protected virtual void AddParameters(IRestRequest request, 
                                             Dictionary<String, String> parameters)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            foreach (var parameter in parameters)
                request.AddParameter(parameter.Key, parameter.Value, ParameterType.QueryString);
        }

        protected virtual void AddBody(IRestRequest request, String body)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (!String.IsNullOrEmpty(body))
                request.AddParameter("application/json", body, ParameterType.RequestBody);
        }

        protected virtual ClientResponse<T> HandleResponse<T>(IRestResponse response)
			where T: class
        {
            ClientResponse<T> clientResponse = null;
            int statusCode = (int)response.StatusCode;

            if (statusCode >= 200 && statusCode < 300)
                clientResponse = HandleNormalResponse <T>(response) as ClientResponse<T>;
            else
                clientResponse = HandleErrorResponse <T>(response) as ClientResponse<T>;

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
                throw new ApiException((int)response.Response.StatusCode, 
                    response.Response.StatusDescription,
                    response.Errors,
                    response.Response.Content);
            }
        }
    }
}