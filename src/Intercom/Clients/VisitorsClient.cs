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
    internal class VisitorsClient : Client
    {
        private const String VISITORS_RESOURCE = "visitors";

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
                throw new ArgumentNullException("'parameters' argument is null.");
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
                throw new ArgumentNullException("'parameters' argument is null.");
            }

            ClientResponse<Visitor> result = null;
            result = Get<Visitor>(resource: VISITORS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;       
        }

        public Visitor View(Visitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException("'visitor' argument is null.");
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
                throw new ArgumentNullException("you need to provide either 'visitor.id', 'visitor.user_id' to view a visitor.");
            }

            return result.Result;   
        }

        public Visitor Update(Visitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException("'visitor' argument is null.");
            }

            if (String.IsNullOrEmpty(visitor.id) && String.IsNullOrEmpty(visitor.user_id))
            {
                throw new ArgumentException("you need to provide either 'visitor.id', 'visitor.user_id' to update a user.");
            }

            ClientResponse<Visitor> result = null;
            result = Put<Visitor>(visitor);

            return result.Result;
        }

        public Visitor Delete(Visitor visitor)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException("'visitor' argument is null.");
            }

            if (String.IsNullOrEmpty(visitor.id))
            {
                throw new ArgumentNullException("'visitor.id' argument is null.");
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
                throw new ArgumentNullException("'id' argument is null.");
            }

            ClientResponse<Visitor> result = null;
            result = Delete<Visitor>(resource: VISITORS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;           
        }

        private Visitor Convert(Visitor visitor, Contact contact)
        {
            if (visitor == null)
            {
                throw new ArgumentNullException("'visitor' argument is null.");
            }

            if (contact == null)
            {
                throw new ArgumentNullException("'contact' argument is null.");
            }

            if (String.IsNullOrEmpty(visitor.id) && String.IsNullOrEmpty(visitor.user_id))
            {
                throw new ArgumentException("you need to provide either 'visitor.id', 'visitor.user_id' to convert a visitor to a lead.");
            }


            if (String.IsNullOrEmpty(contact.id) && String.IsNullOrEmpty(contact.user_id))
            {
                throw new ArgumentException("you need to provide either 'contact.id', 'contact.user_id' to convert a visitor to a lead.");
            }

            throw new NotImplementedException();
        }

        private Visitor Convert(Visitor visitor, User user)
        {
            throw new NotImplementedException();
        }
    }
}