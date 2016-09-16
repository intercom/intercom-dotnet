using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace Intercom.Clients
{
	public class EventsClient : Client
	{
		private const String COMPANIES_RESOURCE = "events";

		public EventsClient (Authentication authentication)
			: base (INTERCOM_API_BASE_URL, COMPANIES_RESOURCE, authentication)
		{
		}

        public EventsClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, COMPANIES_RESOURCE, authentication)
        {
        }

		public Event Create (Event @event)
		{
            if (@event == null)
            {
                throw new ArgumentNullException (nameof(@event));
            }

            if (String.IsNullOrEmpty(@event.event_name))
            {
                throw new ArgumentException ("'event_name' argument is null or empty.");
            }

            if (!@event.created_at.HasValue)
            {
                throw new ArgumentException ("'created_at' argument must have value.");
            }

			ClientResponse<Event> result = null;
            result = Post<Event> (@event);
			return result.Result;
		}

        public Events List (User user)
		{
			Dictionary<String, String> parameters = new Dictionary<string, string> ();
            parameters.Add (Constants.TYPE, "user");

			ClientResponse<Events> result = null;

            if (!String.IsNullOrEmpty (user.id)) {
                parameters.Add (Constants.INTERCOM_USER_ID, user.id);
            } else if (!String.IsNullOrEmpty (user.user_id)) {
                parameters.Add (Constants.USER_ID, user.user_id);
            } else if (!String.IsNullOrEmpty (user.email)) {
                parameters.Add (Constants.EMAIL, user.email);
			} else {
				throw new ArgumentException (String.Format ("you should provide at least value for one of these parameters {0}, or {1}, or {2} .", Constants.INTERCOM_USER_ID, Constants.USER_ID, Constants.EMAIL));
			}

			result = Get<Events> (parameters: parameters);
			return result.Result;
		}

		public Events List (Dictionary<String, String> parameters)
		{
			if (parameters == null) {
				throw new ArgumentNullException (nameof(parameters));
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