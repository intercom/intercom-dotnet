using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Intercom.Clients;
using Intercom.Core;
using Intercom.Data;
using Intercom.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace Intercom.Clients
{
    public class NotesClient : Client
    {
        private const String NOTES_RESOURCE = "notes";

        public NotesClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, NOTES_RESOURCE, authentication)
        {
        }

        public NotesClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, NOTES_RESOURCE, authentication)
        {
        }

        public Note Create(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            if (note.user == null)
            {
                throw new ArgumentException("'note.user' argument is null.");
            }

            object u = null;

            if (!String.IsNullOrEmpty(note.user.id))
                u = new { id = note.user.id };
            else if (!String.IsNullOrEmpty(note.user.user_id))
                u = new { user_id = note.user.user_id };
            else if (!String.IsNullOrEmpty(note.user.email))
                u = new { email = note.user.email };
            else
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");

            if (String.IsNullOrEmpty(note.body))
            {
                throw new ArgumentException("'note.body' argument is null or empty.");
            }

            if (note.author == null)
            {
                throw new ArgumentException("'note.author' argument is null.");
            }

            if (String.IsNullOrEmpty(note.author.id))
            {
                throw new ArgumentException("'note.author.id' argument is null.");
            }

            ClientResponse<Note> result = null;

            var body = new {
                admin_id = note.author.id,
                body = note.body,
                user = u
            };

            String b = JsonConvert.SerializeObject(body,
                Formatting.None, 
                new JsonSerializerSettings
                { 
                    NullValueHandling = NullValueHandling.Ignore
                });

            result = Post<Note>(b);
            return result.Result;
        }

        public Note Create(User user, String body, String adminId = null)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            object u = null;

            if (!String.IsNullOrEmpty(user.id))
                u = new { id = user.id };
            else if (!String.IsNullOrEmpty(user.user_id))
                u = new { user_id = user.user_id };
            else if (!String.IsNullOrEmpty(user.email))
                u = new { email = user.email };
            else
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");

            if (String.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException(nameof(body));
            }

            ClientResponse<Note> result = null;

            var note = new {
                admin_id = adminId,
                body = body,
                user = u
            };

            String b = JsonConvert.SerializeObject(note,
                           Formatting.None, 
                           new JsonSerializerSettings
                { 
                    NullValueHandling = NullValueHandling.Ignore
                });

            result = Post<Note>(b);

            return result.Result;
        }

        public Note View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            ClientResponse<Note> result = null;
            result = Get<Note>(resource: NOTES_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;       
        }

        public Notes List(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Dictionary<String, String> parameters = new Dictionary<String, String>();
            ClientResponse<Notes> result = null;

            if (!String.IsNullOrEmpty(user.id))
            {
                parameters.Add(Constants.ID, user.id);
                result = Get<Notes>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.user_id))
            {
                parameters.Add(Constants.USER_ID, user.user_id);
                result = Get<Notes>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.email))
            {
                parameters.Add(Constants.EMAIL, user.email);
                result = Get<Notes>(parameters: parameters);         
            }
            else
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            return result.Result;
        }

        // TODO: Add List function for Notes to allow pagination
    }
}