using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using RestSharp.Authenticators;

namespace Library
{
	public class EventsClient : Client
	{
		private const String COMPANIES_RESOURCE = "events";

		public EventsClient (Authentication authentication)
			: base (INTERCOM_API_BASE_URL, COMPANIES_RESOURCE, authentication)
		{
		}

		public Event Create (Event ev)
		{
			ClientResponse<Event> result = null;
			result = Post<Event> (ev);
			return result.Result;
		}

		public Events List (String intercomUserId = null, String userId = null, String email = null)
		{
			Dictionary<String, String> parameters = new Dictionary<string, string> ();
			ClientResponse<Events> result = null;

			if (!String.IsNullOrEmpty (intercomUserId)) {
				parameters.Add (Constants.INTERCOM_USER_ID, intercomUserId);
			} else if (!String.IsNullOrEmpty (userId)) {
				parameters.Add (Constants.USER_ID, userId);
			} else if (!String.IsNullOrEmpty (email)) {
				parameters.Add (Constants.EMAIL, email);
			} else {
				throw new ArgumentException (String.Format ("you should provide at least value for one of these parameters {0}, or {1}, or {2} .", Constants.INTERCOM_USER_ID, Constants.USER_ID, Constants.EMAIL));
			}

			parameters.Add (Constants.TYPE, "user");

			result = Get<Events> (parameters: parameters);
			return result.Result;
		}

		public Events List (Dictionary<String, String> parameters)
		{
			if (parameters == null) {
				throw new ArgumentNullException ("'parameters' argument is null.");
			}

			if (!parameters.Any ()) {
				throw new ArgumentException ("'parameters' argument should include at least one parameter key, value.");
			}

			if (!parameters.ContainsKey (Constants.INTERCOM_USER_ID) &&
			    !parameters.ContainsKey (Constants.USER_ID) &&
			    !parameters.ContainsKey (Constants.EMAIL)) {
				throw new ArgumentException (String.Format ("'parameters' argument should include at least {0}, or {1}, or {2} parameter.", Constants.INTERCOM_USER_ID, Constants.USER_ID, Constants.EMAIL));
			}

			if (!parameters.Contains (new KeyValuePair<string, string> (Constants.TYPE, "user"))) {
				parameters.Add (Constants.TYPE, "user");
			}
				
			ClientResponse<Events> result = null;
			result = Get<Events> (parameters: parameters);
			return result.Result;
		}
	}
}