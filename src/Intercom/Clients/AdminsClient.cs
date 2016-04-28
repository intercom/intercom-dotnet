using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Library.Core;
using RestSharp;
using RestSharp.Authenticators;

namespace Library.Clients
{
    public class AdminsClient : Client
    {
        private const String ADMINS_RESOURCE = "admins";

        public AdminsClient (Authentication authentication)
            : base (INTERCOM_API_BASE_URL, ADMINS_RESOURCE, authentication)
        {
        }

        public Admins List ()
        {
            ClientResponse<Admins> result = null;
            result = Get<Admins> ();
            return result.Result;
        }

        public Admin View (String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException ("'id' argument is null or empty.");
            }

            ClientResponse<Admin> result = null;
            result = Get<Admin> (resource: ADMINS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;
        }

        public Admin View(Admin admin)
        {
            if (admin == null) {
                throw new ArgumentNullException ("'admin' argument is null.");
            }

            if (String.IsNullOrEmpty(admin.id))
            {
                throw new ArgumentNullException ("you must provide value for 'admin.id'.");
            }

            ClientResponse<Admin> result = null;
            result = Get<Admin> (resource: ADMINS_RESOURCE + Path.DirectorySeparatorChar + admin.id);
            return result.Result;  
        }
    }
}