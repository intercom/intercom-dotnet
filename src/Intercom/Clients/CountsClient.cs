using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;

namespace Intercom.Clients
{
	public class CountsClient : Client
	{
		private const String COUNTS_RESOURCE = "counts";

		public CountsClient(Authentication authentication)
			: base(INTERCOM_API_BASE_URL, COUNTS_RESOURCE, authentication)
		{
		}

		public CountsClient(String intercomApiUrl, Authentication authentication)
			: base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, COUNTS_RESOURCE, authentication)
		{
		}

		public async Task<AppCount> GetAppCount()
		{
			ClientResponse<AppCount> result = null;
			result = await Get<AppCount>();
			return result.Result;
		}

		public async Task<ConversationAppCount> GetConversationAppCount()
		{
			Dictionary<String, String> parameters = new Dictionary<string, string>();
			parameters.Add(Constants.TYPE, Constants.CONVERSATION);

			ClientResponse<ConversationAppCount> result = null;
			result = await Get<ConversationAppCount>(parameters: parameters);
			return result.Result;
		}

		public async Task<ConversationAdminCount> GetConversationAdminCount()
		{
			Dictionary<String, String> parameters = new Dictionary<string, string>();
			parameters.Add(Constants.TYPE, Constants.CONVERSATION);
			parameters.Add(Constants.COUNT, Constants.ADMIN);

			ClientResponse<ConversationAdminCount> result = null;
			result = await Get<ConversationAdminCount>(parameters: parameters);
			return result.Result;
		}

		public async Task<UserTagCount> GetUserTagCount()
		{
			Dictionary<String, String> parameters = new Dictionary<string, string>();
			parameters.Add(Constants.TYPE, Constants.USER);
			parameters.Add(Constants.COUNT, Constants.TAG);

			ClientResponse<UserTagCount> result = null;
			result = await Get<UserTagCount>(parameters: parameters);
			return result.Result;
		}

		public async Task<UserSegmentCount> GetUserSegmentCount()
		{
			Dictionary<String, String> parameters = new Dictionary<string, string>();
			parameters.Add(Constants.TYPE, Constants.USER);
			parameters.Add(Constants.COUNT, Constants.SEGMENT);

			ClientResponse<UserSegmentCount> result = null;
			result = await Get<UserSegmentCount>(parameters: parameters);
			return result.Result;
		}

		public async Task<CompanySegmentCount> GetCompanySegmentCount()
		{
			Dictionary<String, String> parameters = new Dictionary<string, string>();
			parameters.Add(Constants.TYPE, Constants.COMPANY);
			parameters.Add(Constants.COUNT, Constants.SEGMENT);

			ClientResponse<CompanySegmentCount> result = null;
			result = await Get<CompanySegmentCount>(parameters: parameters);
			return result.Result;
		}

		public async Task<CompanyTagCount> GetCompanyTagCount()
		{
			Dictionary<String, String> parameters = new Dictionary<string, string>();
			parameters.Add(Constants.TYPE, Constants.COMPANY);
			parameters.Add(Constants.COUNT, Constants.TAG);

			ClientResponse<CompanyTagCount> result = null;
			result = await Get<CompanyTagCount>(parameters: parameters);
			return result.Result;
		}

		public async Task<CompanyUserCount> GetCompanyUserCount()
		{
			Dictionary<String, String> parameters = new Dictionary<string, string>();
			parameters.Add(Constants.TYPE, Constants.COMPANY);
			parameters.Add(Constants.COUNT, Constants.USER);

			ClientResponse<CompanyUserCount> result = null;
			result = await Get<CompanyUserCount>(parameters: parameters);
			return result.Result;
		}
	}
}