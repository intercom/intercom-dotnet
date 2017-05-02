using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using System.Text;

namespace Intercom.Core
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
		protected const String USER_AGENT_HEADER = "User-Agent";
		protected const String USER_AGENT_VALUE = "intercom-dotnet/2.0.0";

		protected readonly String URL;
		protected readonly String RESRC;
		protected readonly Authentication AUTH;

		static HttpClient httpClient;

		public Client(String url, String resource, Authentication authentication)
		{
			if (authentication == null)
				throw new ArgumentNullException(nameof(authentication));

			if (String.IsNullOrEmpty(url))
				throw new ArgumentNullException(nameof(url));

			if (String.IsNullOrEmpty(resource))
				throw new ArgumentNullException(nameof(resource));

			this.URL = url;
			this.RESRC = resource;
			this.AUTH = authentication;
			httpClient = new HttpClient();
			httpClient.BaseAddress = new Uri(URL);
		}

		protected virtual async Task<ClientResponse<T>> Get<T>(
			Dictionary<String, String> headers = null,
			Dictionary<String, String> parameters = null,
			String resource = null)
			where T : class
		{
			ClientResponse<T> clientResponse = null;

			try
			{
				BuildClient();
				HttpRequestMessage request = BuildRequest(headers: headers, parameters: parameters, resource: resource);
				request.Method = HttpMethod.Get;

				HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
				clientResponse = await HandleResponse<T>(response);
			}
			catch (ApiException ex)
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
						"GET", URL, RESRC, resource), ex);
			}

			return clientResponse;
		}

		protected virtual async Task<ClientResponse<T>> Post<T>(String body,
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

			try
			{
				BuildClient();
				HttpRequestMessage request = BuildRequest(headers: headers, parameters: parameters, body: body, resource: resource);
				request.Method = HttpMethod.Post;

				HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
				clientResponse = await HandleResponse<T>(response);
			}
			catch (ApiException ex)
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
						"POST", URL, RESRC, resource, body), ex);
			}

			return clientResponse;
		}

		protected virtual async Task<ClientResponse<T>> Post<T>(T body,
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

			try
			{
				String requestBody = Serialize<T>(body);
				BuildClient();

				HttpRequestMessage request = BuildRequest(headers: headers, parameters: parameters, body: requestBody, resource: resource);
				request.Method = HttpMethod.Post;

				HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
				clientResponse = await HandleResponse<T>(response);
			}
			catch (ApiException ex)
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
						"POST", URL, RESRC, resource, typeof(T)), ex);
			}

			return clientResponse;
		}

		protected virtual async Task<ClientResponse<T>> Put<T>(String body,
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

			try
			{
				BuildClient();

				HttpRequestMessage request = BuildRequest(headers: headers, parameters: parameters, body: body, resource: resource);
				request.Method = HttpMethod.Put;

				HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
				clientResponse = await HandleResponse<T>(response);
			}
			catch (ApiException ex)
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
						"POST", URL, RESRC, resource, body), ex);
			}

			return clientResponse;
		}

		protected virtual async Task<ClientResponse<T>> Put<T>(T body,
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

			try
			{
				String requestBody = Serialize<T>(body);
				BuildClient();

				HttpRequestMessage request = BuildRequest(headers: headers, parameters: parameters, body: requestBody, resource: resource);
				request.Method = HttpMethod.Put;

				HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
				clientResponse = await HandleResponse<T>(response);
			}
			catch (ApiException ex)
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
						"POST", URL, RESRC, resource), ex);
			}

			return clientResponse;
		}

		protected virtual async Task<ClientResponse<T>> Delete<T>(
			Dictionary<String, String> headers = null,
			Dictionary<String, String> parameters = null,
			String resource = null)
			where T : class
		{
			ClientResponse<T> clientResponse = null;

			try
			{
				BuildClient();

				HttpRequestMessage request = BuildRequest(headers: headers, parameters: parameters, resource: resource);
				request.Method = HttpMethod.Delete;

				HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);
				clientResponse = await HandleResponse<T>(response);
			}
			catch (ApiException ex)
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
						"POST", URL, RESRC, resource), ex);
			}

			return clientResponse;
		}

		protected virtual HttpRequestMessage BuildRequest(Dictionary<String, String> headers = null,
													Dictionary<String, String> parameters = null,
													String body = null,
													String resource = null)
		{
			String final = String.IsNullOrEmpty(resource) ? RESRC : resource;

			HttpRequestMessage request = new HttpRequestMessage();//final, httpMethod);
			request.Headers.Add(CONTENT_TYPE_HEADER, CONTENT_TYPE_VALUE);
			request.Headers.Add(CONTENT_TYPE_HEADER, CONTENT_TYPE_VALUE);
			request.Headers.Add(ACCEPT_CHARSET_HEADER, ACCEPT_CHARSET_VALUE);
			request.Headers.Add(ACCEPT_HEADER, ACCEPT_VALUE);
			request.Headers.Add(USER_AGENT_HEADER, USER_AGENT_VALUE);

			if (headers != null && headers.Any())
				AddHeaders(request, headers);

			if (parameters != null && parameters.Any())
				AddParameters(request, parameters);

			if (!String.IsNullOrEmpty(body))
				AddBody(request, body);

			return request;
		}

		protected virtual void BuildClient()
		{
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
				"Basic",
				Convert.ToBase64String(
					System.Text.Encoding.UTF8.GetBytes(
						string.Format("{0}:{1}", AUTH.PersonalAccessToken, string.Empty))));

			//RestClient client = new RestClient(URL);

			//if (!String.IsNullOrEmpty(AUTH.AppId) && !String.IsNullOrEmpty(AUTH.AppKey))
			//	client.Authenticator = new HttpBasicAuthenticator(AUTH.AppId, AUTH.AppKey);
			//else
			//	client.Authenticator = new HttpBasicAuthenticator(AUTH.PersonalAccessToken, String.Empty);

			//return client;
		}

		protected virtual void AddHeaders(HttpRequestMessage request,
										  Dictionary<String, String> headers)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (headers == null)
				throw new ArgumentNullException(nameof(headers));

			foreach (var header in headers)
				request.Headers.Add(header.Key, header.Value);
		}

		protected virtual void AddParameters(HttpRequestMessage request,
											 Dictionary<String, String> parameters)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (parameters == null)
				throw new ArgumentNullException(nameof(parameters));

			var encodedContent = new FormUrlEncodedContent(parameters);
			request.Content = encodedContent;
		}

		protected virtual void AddBody(HttpRequestMessage request, String body)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (!String.IsNullOrEmpty(body))
			{
				request.Content = new StringContent(body);
			}
		}

		protected virtual async Task<ClientResponse<T>> HandleResponse<T>(HttpResponseMessage response)
			where T : class
		{
			ClientResponse<T> clientResponse = null;
			int statusCode = (int)response.StatusCode;

			if (statusCode >= 200 && statusCode < 300)
				clientResponse = await HandleNormalResponse<T>(response).ConfigureAwait(false) as ClientResponse<T>;
			else
				clientResponse = await HandleErrorResponse<T>(response).ConfigureAwait(false) as ClientResponse<T>;

			await AssertIfAnyErrors(clientResponse).ConfigureAwait(false);

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

		protected async Task<ClientResponse<T>> HandleErrorResponse<T>(HttpResponseMessage response)
			where T : class
		{
			var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			if (String.IsNullOrEmpty(content))
			{
				return new ClientResponse<T>(response: response);
			}
			else
			{
				Errors errors = Deserialize<Errors>(content);
				return new ClientResponse<T>(response: response, errors: errors);
			}
		}

		protected async Task<ClientResponse<T>> HandleNormalResponse<T>(HttpResponseMessage response)
			where T : class
		{
			return new ClientResponse<T>(response: response, result: Deserialize<T>(await response.Content.ReadAsStringAsync()));
		}

		protected async Task AssertIfAnyErrors<T>(ClientResponse<T> response)
			where T : class
		{
			if (response.Errors != null && response.Errors.errors != null && response.Errors.errors.Any())
			{
				throw new ApiException((int)response.Response.StatusCode,
									   response.Response.ReasonPhrase,
										response.Errors,
									   await response.Response.Content.ReadAsStringAsync());
			}
		}
	}
}