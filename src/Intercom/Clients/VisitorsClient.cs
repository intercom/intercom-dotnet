using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;

namespace Intercom.Clients
{
    public class VisitorsClient : Client
    {
        private const String VISITORS_RESOURCE = "visitors";
        private const String VISITORS_CONVERT = "convert";

        public VisitorsClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, VISITORS_RESOURCE, authentication)
        {
        }

        public VisitorsClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, VISITORS_RESOURCE, authentication)
        {
        }

        public Visitor View(Dictionary<String, String> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            if (!parameters.Any())
            {
                throw new ArgumentException("'parameters' argument should include user_id parameter.");
            }

            ClientResponse<Visitor> result = null;

            result = Get<Visitor>(parameters: parameters);
            return result.Result;
        }

        public Visitor View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            ClientResponse<Visitor> result = null;
            result = Get<Visitor>(resource: VISITORS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;       
        }

        public Visitor View(Visitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<Visitor> result = null;

            if (!String.IsNullOrEmpty(visitor.id))
            {
                result = Get<Visitor>(resource: VISITORS_RESOURCE + Path.DirectorySeparatorChar + visitor.id);
            }
            else if (!String.IsNullOrEmpty(visitor.user_id))
            {
                parameters.Add(Constants.USER_ID, visitor.user_id);
                result = Get<Visitor>(parameters: parameters);
            }
            else
            {
                throw new ArgumentException("you need to provide either 'visitor.id', 'visitor.user_id' to view a visitor.");
            }

            return result.Result;   
        }

        public Visitor Update(Visitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            if (String.IsNullOrEmpty(visitor.id) && String.IsNullOrEmpty(visitor.user_id))
            {
                throw new ArgumentException("you need to provide either 'visitor.id', 'visitor.user_id' to update a visitor.");
            }

            ClientResponse<Visitor> result = null;
            result = Put<Visitor>(visitor);

            return result.Result;
        }

        public Visitor Delete(Visitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            if (String.IsNullOrEmpty(visitor.id))
            {
                throw new ArgumentException("'visitor.id' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<Visitor> result = null;
            result = Delete<Visitor>(resource: VISITORS_RESOURCE + Path.DirectorySeparatorChar + visitor.id);
            return result.Result;       
        }

        public Visitor Delete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            ClientResponse<Visitor> result = null;
            result = Delete<Visitor>(resource: VISITORS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;           
        }

        public User ConvertToUser(Visitor visitor, User user)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            object userBody = null;

            if (!String.IsNullOrEmpty (user.id))
                userBody = new { id = user.id };
            else if (!String.IsNullOrEmpty (user.user_id))
                userBody = new { user_id = user.user_id };
            else if (!String.IsNullOrEmpty (user.email))
                userBody = new { email = user.email };
            else
                throw new ArgumentException ("you need to provide either 'user.id', 'user.user_id', or 'user.email' to convert a visitor.");

            object visitorBody = null;

            if (!String.IsNullOrEmpty (visitor.id))
                visitorBody = new { id = visitor.id};
            else if (!String.IsNullOrEmpty (visitor.user_id))
                visitorBody = new {user_id = visitor.user_id };
            else if (!String.IsNullOrEmpty (visitor.email))
                visitorBody = new { email = visitor.email };
            else
                throw new ArgumentException ("you need to provide either 'visitor.id', 'visitor.user_id', or 'visitor.email' to convert a visitor.");

            var body = new {
                visitor = visitorBody,
                user = userBody,
                type = "user"
            };

            String b = JsonConvert.SerializeObject (body,
                Formatting.None,
                new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore
                });

            ClientResponse<User> result = null;
            result = Post<User>(b, resource: VISITORS_RESOURCE + Path.DirectorySeparatorChar + VISITORS_CONVERT);
            return result.Result;
        }

        public Contact ConvertToContact(Visitor visitor)
        {
            if (visitor == null) {
                throw new ArgumentNullException (nameof(visitor));
            }

            object visitorBody = null;

            if (!String.IsNullOrEmpty (visitor.id))
                visitorBody = new { id = visitor.id };
            else if (!String.IsNullOrEmpty (visitor.user_id))
                visitorBody = new { user_id = visitor.user_id };
            else if (!String.IsNullOrEmpty (visitor.email))
                visitorBody = new { email = visitor.email };
            else
                throw new ArgumentException ("you need to provide either 'visitor.id', 'visitor.user_id', or 'visitor.email' to convert a visitor.");

            var body = new {
                visitor = visitorBody,
                type = "lead"
            };

            String b = JsonConvert.SerializeObject (body,
                Formatting.None,
                new JsonSerializerSettings {
                    NullValueHandling = NullValueHandling.Ignore
                });

            ClientResponse<Contact> result = null;
            result = Post<Contact> (b, resource: VISITORS_RESOURCE + Path.DirectorySeparatorChar + VISITORS_CONVERT);
            return result.Result;
        }
    }
}