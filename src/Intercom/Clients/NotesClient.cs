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
                throw new ArgumentNullException("'note' argument is null.");
            }

            if (note.user == null)
            {
                throw new ArgumentNullException("'note.user' shouldnt be null.");
            }

            if (String.IsNullOrEmpty(note.user.id) && String.IsNullOrEmpty(note.user.user_id) && string.IsNullOrEmpty(note.user.email))
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to create a user.");
            }

            if (String.IsNullOrEmpty(note.body))
            {
                throw new ArgumentNullException("'note.body' shouldnt be null.");
            }

            ClientResponse<Note> result = null;
            result = Post<Note>(note);
            return result.Result;
        }

        public Note Create(User user, String body, String adminId = null)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'note' argument is null.");
            }

            if (String.IsNullOrEmpty(user.id) && String.IsNullOrEmpty(user.user_id) && string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to create a user.");
            }

            if (String.IsNullOrEmpty(body))
            {
                throw new ArgumentNullException("'body' argument is null or empty.");
            }

            ClientResponse<Note> result = null;
            result = Post<Note>(new Note() {
                author = new Author() { id = adminId }, 
                body = body,
                user = new User() { id = user.id, user_id = user.user_id, email = user.email }
            });

            return result.Result;
        }

        public Note View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'id' argument is null or empty.");
            }

            ClientResponse<Note> result = null;
            result = Get<Note>(resource: NOTES_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;       
        }

        public Notes List (User user)
        {
            if (user == null) {
                throw new ArgumentNullException ("'user' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<String, String> ();
            ClientResponse<Notes> result = null;

            if (!String.IsNullOrEmpty (user.id)) {
                result = Get<Notes> (resource: NOTES_RESOURCE + Path.DirectorySeparatorChar + user.id);
            } else if (!String.IsNullOrEmpty (user.user_id)) {
                parameters.Add (Constants.USER_ID, user.id);
                result = Get<Notes> (parameters: parameters);
            } else if (!String.IsNullOrEmpty (user.email)) {
                parameters.Add (Constants.EMAIL, user.email);
                result = Get<Notes> (parameters: parameters);         
            } else {
                throw new ArgumentException ("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            return result.Result;
        }

        // TODO: Add List function for Notes to allow pagination
    }
}