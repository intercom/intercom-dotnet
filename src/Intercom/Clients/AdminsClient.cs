using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace Intercom.Clients
{
    public class AdminsClient : Client
    {
        private const String ADMINS_RESOURCE = "admins";

        public AdminsClient (Authentication authentication)
            : base (INTERCOM_API_BASE_URL, ADMINS_RESOURCE, authentication)
        {
        }

        public AdminsClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, ADMINS_RESOURCE, authentication)
        {
        }

        public Admins List ()
        {
            ClientResponse<Admins> result = null;
            result = Get<Admins> ();
            return result.Result;
        }

        public Admins List (Dictionary<String, String> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException (nameof(parameters));
            }

            if (!parameters.Any())
            {
                throw new ArgumentException ("'parameters' argument is empty.");
            }

            ClientResponse<Admins> result = null;
            result = Get<Admins> ();
            return result.Result;
        }

        public Admin View (String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException (nameof(id));
            }

            ClientResponse<Admin> result = null;
            result = Get<Admin> (resource: ADMINS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;
        }

        public Admin View(Admin admin)
        {
            if (admin == null) {
                throw new ArgumentNullException (nameof(admin));
            }

            if (String.IsNullOrEmpty(admin.id))
            {
                throw new ArgumentException ("you must provide value for 'admin.id'.");
            }

            ClientResponse<Admin> result = null;
            result = Get<Admin> (resource: ADMINS_RESOURCE + Path.DirectorySeparatorChar + admin.id);
            return result.Result;  
        }
    }
}