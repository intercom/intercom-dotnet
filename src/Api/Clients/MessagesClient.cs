using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using RestSharp.Authenticators;

namespace Library
{
	public class MessagesClient : Client
	{
		public MessagesClient (String baseUrl, String resource, Authentication authentication)
			: base (baseUrl, resource, authentication)
		{
		}

        public UserConversationMessage Create (UserConversationMessage userMessage)
		{
            ClientResponse<UserConversationMessage> result = null;
            result = Post<UserConversationMessage> (userMessage);
			return result.Result;
		}

        public AdminConversationMessage Create (AdminConversationMessage adminMessage)
		{
            ClientResponse<AdminConversationMessage> result = null;
            result = Post<AdminConversationMessage> (adminMessage);
			return result.Result;
		}

		public User View (Dictionary<String, String> parameters)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("'parameters' argument is null.");
			}

			if (!parameters.Any ()) {
				throw new ArgumentException ("'parameters' argument should include user_id parameter.");
			}

			ClientResponse<User> result = null;

			result = Get<User> (parameters: parameters);
			return result.Result;
		}
	}
}