using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library.Core;
using RestSharp;
using RestSharp.Authenticators;
using System.IO;
using Newtonsoft.Json;

namespace Library
{
    public class TagsClient: Client
    {
        public enum TagType
        {
            User,
            Contact,
            Company
        }

        private const String TAGS_RESOURCE = "tags";

        public TagsClient (Authentication authentication)
            : base (INTERCOM_API_BASE_URL, TAGS_RESOURCE, authentication)
        {
        }

        public Tag Create (Tag tag)
        {
            if (tag == null) {
                throw new ArgumentNullException ("'tag' argument is null.");
            }

            if (string.IsNullOrEmpty(tag.name))
            {
                throw new ArgumentNullException ("you need to provide 'tag.name' to create a tag.");
            }

            ClientResponse<Tag> result = null;
            result = Post<Tag> (tag);
            return result.Result;
        }

        public Tag Update (Tag tag)
        {
            if (tag == null) {
                throw new ArgumentNullException ("'contact' argument is null.");
            }

            if (String.IsNullOrEmpty(tag.name) || String.IsNullOrEmpty(tag.id))
            {
                throw new ArgumentException("you need to provide 'tag.id', 'tag.name' to update a tag.");
            }

            ClientResponse<Tag> result = null;
            result = Post<Tag> (tag);
            return result.Result;
        }

        public Tag View (String id)
        {
            if (String.IsNullOrEmpty (id)) {
                throw new ArgumentNullException ("'parameters' argument is null.");
            }

            ClientResponse<Tag> result = null;
            result = Get<Tag> (resource: TAGS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;       
        }

        public Tag View (Tag tag)
        {
            if (tag == null) {
                throw new ArgumentNullException ("'tag' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string> ();
            ClientResponse<Tag> result = null;

            if (!String.IsNullOrEmpty (tag.id)) {
                result = Get<Tag> (resource: TAGS_RESOURCE + Path.DirectorySeparatorChar + tag.id);
            } else {
                throw new ArgumentNullException ("you need to provide 'tag.id' to view a tag.");
            }

            return result.Result;   
        }

        public Tags List ()
        {
            ClientResponse<Tags> result = null;
            result = Get<Tags> ();
            return result.Result;
        }

        public void Delete (Tag tag)
        {
            if (tag == null) {
                throw new ArgumentNullException ("'tag' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string> ();
            ClientResponse<Tag> result = null;

            if (!String.IsNullOrEmpty (tag.id)) {
                result = Delete<Tag> (resource: TAGS_RESOURCE + Path.DirectorySeparatorChar + tag.id);
            } else {
                throw new ArgumentNullException ("you need to provide 'tag.id' to delete a tag.");
            }
        }

        public void Delete (String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'id' argument is null or empty.");
            }

            ClientResponse<Tag> result = null;
            Delete<Tag>(resource: TAGS_RESOURCE + Path.DirectorySeparatorChar + id);
        }

        public Tag Tag(String name, List<Company> companies)
        {
            if (companies == null) {
                throw new ArgumentNullException ("'companies' argument is null.");
            }

            if (!companies.Any ()) {
                throw new ArgumentException ("'companies' argument should include more than one company.");
            }

            foreach (var c in companies)
                if (String.IsNullOrEmpty(c.id) && String.IsNullOrEmpty(c.company_id))
                    throw new ArgumentException ("you need to provide either 'company.id', 'company.company_id' to tag a company.");

            ClientResponse<Tag> result = null;

            var tags = new { name = name, companies = companies.Select(c => new { id = c.id }) };
            String body = JsonConvert.SerializeObject(tags);
            result = Post<Tag>(body);

            return result.Result;
        }

        public Tag Tag(String name, List<User> users)
        {
            if (users == null) {
                throw new ArgumentNullException ("'users' argument is null.");
            }

            if (!users.Any ()) {
                throw new ArgumentException ("'users' argument should include more than one user.");
            }

            ClientResponse<Tag> result = null;
            String body = CreateTagBody(name, false, users: users);
            result = Post<Tag>(body);

            return result.Result;
        }

        public Tag Tag(String name, List<Contact> leads)
        {
            if (leads == null) {
                throw new ArgumentNullException ("'users' argument is null.");
            }

            if (!leads.Any ()) {
                throw new ArgumentException ("'users' argument should include more than one user.");
            }

            ClientResponse<Tag> result = null;
            String body = CreateTagBody(name, false, leads: leads);
            result = Post<Tag>(body);

            return result.Result;
        }

        public Tag Tag(String name, List<String> ids, TagType tagType)
        {
            switch (tagType)
            {
                case TagType.Company:
                    return Tag(name, ids.Select(id => new Company() { id = id }).ToList());
                    break;
                case TagType.Contact:
                    return Tag(name, ids.Select(id => new Contact() { id = id }).ToList());
                    break;
                case TagType.User:
                    return Tag(name, ids.Select(id => new User() { id = id }).ToList());
                    break;
                default:
                    return Tag(name, ids.Select(id => new User() { id = id }).ToList());
                    break;
            }
        }

        public Tag UnTag(String name, List<User> users)
        {
            if (users == null) {
                throw new ArgumentNullException ("'users' argument is null.");
            }

            if (!users.Any ()) {
                throw new ArgumentException ("'users' argument should include more than one user.");
            }

            ClientResponse<Tag> result = null;
            String body = CreateTagBody(name, true, users: users);
            result = Post<Tag>(body);

            return result.Result;
        }

        public Tag UnTag(String name, List<Company> companies)
        {
            if (companies == null) {
                throw new ArgumentNullException ("'companies' argument is null.");
            }

            if (!companies.Any ()) {
                throw new ArgumentException ("'companies' argument should include more than one company.");
            }

            foreach (var c in companies)
                if (String.IsNullOrEmpty(c.id) && String.IsNullOrEmpty(c.company_id))
                    throw new ArgumentException ("you need to provide either 'company.id', 'company.company_id' to tag a company.");

            ClientResponse<Tag> result = null;

            var tags = new { name = name, companies = companies.Select(c => new { id = c.id, untag = true}) };
            String body = JsonConvert.SerializeObject(tags);
            result = Post<Tag>(body);

            return result.Result;
        }

        public Tag UnTag(String name, List<Contact> leads)
        {
            if (leads == null) {
                throw new ArgumentNullException ("'users' argument is null.");
            }

            if (!leads.Any ()) {
                throw new ArgumentException ("'users' argument should include more than one user.");
            }

            ClientResponse<Tag> result = null;
            String body = CreateTagBody(name, true, leads: leads);
            result = Post<Tag>(body);

            return result.Result;
        }

        public Tag UnTag(String name, List<String> ids, TagType tagType)
        {
            switch (tagType)
            {
                case TagType.Company:
                    return UnTag(name, ids.Select(id => new Company() { id = id }).ToList());
                    break;
                case TagType.Contact:
                    return UnTag(name, ids.Select(id => new Contact() { id = id }).ToList());
                    break;
                case TagType.User:
                    return UnTag(name, ids.Select(id => new User() { id = id }).ToList());
                    break;
                default:
                    return UnTag(name, ids.Select(id => new User() { id = id }).ToList());
                    break;
            }
        }

        private String CreateTagBody(String name, bool untag, List<User> users = null, List<Contact> leads = null)
        {
            if (users == null)
            {
                foreach (var l in leads)
                    if (String.IsNullOrEmpty(l.id) && String.IsNullOrEmpty(l.user_id) && string.IsNullOrEmpty(l.email))
                        throw new ArgumentException("you need to provide either 'lead.id', 'lead.user_id', 'lead.email' to tag a user.");

                users = leads.Select(l => new User() { id = l.id, user_id = l.user_id, email = l.email }).ToList();
            }
            else
            {
                foreach (var u in users)
                    if (String.IsNullOrEmpty(u.id) && String.IsNullOrEmpty(u.user_id) && string.IsNullOrEmpty(u.email))
                        throw new ArgumentException ("you need to provide either 'user.id', 'user.user_id', 'user.email' to tag a user.");
            }

            object tags = null;

            if(untag)
                tags = new { name = name, users =  users.Select(u => new { id = u.id, user_id = u.user_id, email = u.email, untag = true })};
            else
                tags = new { name = name, users =  users.Select(u => new { id = u.id, user_id = u.user_id, email = u.email }) };

            String body = JsonConvert.SerializeObject(tags);

            return body;
        }
    }
}