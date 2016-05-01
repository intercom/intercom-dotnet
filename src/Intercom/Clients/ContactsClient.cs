using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Library.Clients;
using Library.Core;
using Library.Core;
using Library.Data;
using Library.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace Library.Clients
{
    public class ContactsClient : Client
    {
        public static class ContactSortBy
        {
            public const String created_at = "created_at";
            public const String updated_at = "updated_at";
            public const String signed_up_at = "signed_up_at";
        }

        private const String CONTACTS_RESOURCE = "contacts";

        public ContactsClient (Authentication authentication)
            : base (INTERCOM_API_BASE_URL, CONTACTS_RESOURCE, authentication)
        {
        }

        public ContactsClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, CONTACTS_RESOURCE, authentication)
        {
        }

        public Contact Create (Contact contact)
        {
            if (contact == null) {
                throw new ArgumentNullException ("'contact' argument is null.");
            }

            ClientResponse<Contact> result = null;

            if(contact == null)
                result = Post<Contact> (String.Empty);
            else
                result = Post<Contact> (contact);
            
            return result.Result;
        }

        public Contact Update (Contact contact)
        {
            if (contact == null) {
                throw new ArgumentNullException ("'contact' argument is null.");
            }

            if (String.IsNullOrEmpty(contact.id) && String.IsNullOrEmpty(contact.user_id))
            {
                throw new ArgumentException("you need to provide either 'id', 'user_id', 'email' to view a user.");
            }

            ClientResponse<Contact> result = null;
            result = Post<Contact> (contact);
            return result.Result;
        }

        public Contact View (String id)
        {
            if (String.IsNullOrEmpty (id)) {
                throw new ArgumentNullException ("'parameters' argument is null.");
            }

            ClientResponse<Contact> result = null;
            result = Get<Contact> (resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;       
        }

        public Contact View (Contact contact)
        {
            if (contact == null) {
                throw new ArgumentNullException ("'contact' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string> ();
            ClientResponse<Contact> result = null;

            if (!String.IsNullOrEmpty (contact.id)) {
                result = Get<Contact> (resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + contact.id);
            } else if (!String.IsNullOrEmpty (contact.user_id)) {
                parameters.Add (Constants.USER_ID, contact.user_id);
                result = Get<Contact> (parameters: parameters);
            } else {
                throw new ArgumentException ("you need to provide either 'contact.id', 'contact.user_id' to view a contact.");
            }

            return result.Result;
        }

        public Contacts List ()
        {
            ClientResponse<Contacts> result = null;
            result = Get<Contacts> ();
            return result.Result;
        }

        public Contacts List(String email)
        {
            if (String.IsNullOrEmpty(email)) {
                throw new ArgumentNullException ("'email' argument is null or empty.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.EMAIL, email);

            ClientResponse<Contacts> result = null;
            result = Get<Contacts> (parameters: parameters);
            return result.Result;
        }

        public Contact Delete (Contact contact)
        {
            if (contact == null) {
                throw new ArgumentNullException ("'contact' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string> ();
            ClientResponse<Contact> result = null;

            if (!String.IsNullOrEmpty (contact.id)) {
                result = Delete<Contact> (resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + contact.id);
            } else if (!String.IsNullOrEmpty (contact.user_id)) {
                parameters.Add (Constants.USER_ID, contact.user_id);
                result = Delete<Contact> (parameters: parameters);
            } else {
                throw new ArgumentException ("you need to provide either 'contact.id', 'contact.user_id' to delete a contact.");
            }

            return result.Result;
        }

        public Contact Delete (String id)
        {
            if (String.IsNullOrEmpty (id)) {
                throw new ArgumentNullException ("'id' argument is null.");
            }

            ClientResponse<Contact> result = null;
            result = Delete<Contact> (resource: CONTACTS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;           
        }

        // TODO: Implement converting a lead into a user
        public User Convert(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}