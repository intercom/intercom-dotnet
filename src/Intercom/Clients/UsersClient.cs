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
    public class UsersClient : Client
    {
        public static class UserSortBy
        {
            public const String created_at = "created_at";
            public const String updated_at = "updated_at";
            public const String signed_up_at = "signed_up_at";
        }

        private const String USERS_RESOURCE = "users";

        public UsersClient(Authentication authentication)
            : base(INTERCOM_API_BASE_URL, USERS_RESOURCE, authentication)
        {
        }

        public UsersClient(String intercomApiUrl, Authentication authentication)
            : base(String.IsNullOrEmpty(intercomApiUrl) ? INTERCOM_API_BASE_URL : intercomApiUrl, USERS_RESOURCE, authentication)
        {
        }

        public User Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            if (String.IsNullOrEmpty(user.user_id) && string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentException("you need to provide either 'user.user_id', 'user.email' to create a user.");
            }

            ClientResponse<User> result = null;
            result = Post<User>(user);
            return result.Result;
        }

        public User Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            if (String.IsNullOrEmpty(user.id) && String.IsNullOrEmpty(user.user_id) && string.IsNullOrEmpty(user.email))
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            ClientResponse<User> result = null;
            result = Post<User>(user);

            return result.Result;
        }

        private User CreateOrUpdate(User user)
        {
            ClientResponse<User> result = null;
            result = Post<User>(user);
            return result.Result;
        }

        public User View(Dictionary<String, String> parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("'parameters' argument is null.");
            }

            if (!parameters.Any())
            {
                throw new ArgumentException("'parameters' argument should include user_id parameter.");
            }

            ClientResponse<User> result = null;

            result = Get<User>(parameters: parameters);
            return result.Result;
        }

        public User View(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'parameters' argument is null.");
            }

            ClientResponse<User> result = null;
            result = Get<User>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;		
        }

        public User View(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<User> result = null;

            if (!String.IsNullOrEmpty(user.id))
            {
                result = Get<User>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + user.id);
            }
            else if (!String.IsNullOrEmpty(user.user_id))
            {
                parameters.Add(Constants.USER_ID, user.id);
                result = Get<User>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.email))
            {
                parameters.Add(Constants.EMAIL, user.email);
                result = Get<User>(parameters: parameters);			
            }
            else
            {
                throw new ArgumentNullException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            return result.Result;	
        }

        public Users List()
        {
            ClientResponse<Users> result = null;
            result = Get<Users>();
            return result.Result;
        }

        // TODO: Implement paging (by Pages argument)
        public Users Next(Pages pages)
        {
            return null;
        }

        // TODO: Implement paging
        public Users Next(int page = 1, int perPage = 50, OrderBy orderBy = OrderBy.Dsc, String sortBy = UserSortBy.created_at)
        {
            return null;
        }

        public User Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("'user' argument is null.");
            }

            Dictionary<String, String> parameters = new Dictionary<string, string>();
            ClientResponse<User> result = null;

            if (!String.IsNullOrEmpty(user.id))
            {
                result = Delete<User>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + user.id);
            }
            else if (!String.IsNullOrEmpty(user.user_id))
            {
                parameters.Add(Constants.USER_ID, user.id);
                result = Delete<User>(parameters: parameters);
            }
            else if (!String.IsNullOrEmpty(user.email))
            {
                parameters.Add(Constants.EMAIL, user.email);
                result = Delete<User>(parameters: parameters);			
            }
            else
            {
                throw new ArgumentException("you need to provide either 'user.id', 'user.user_id', 'user.email' to view a user.");
            }

            return result.Result;		
        }

        public User Delete(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("'id' argument is null.");
            }

            ClientResponse<User> result = null;
            result = Delete<User>(resource: USERS_RESOURCE + Path.DirectorySeparatorChar + id);
            return result.Result;			
        }

        // TODO: Implement UpdateLastSeenAt
        private User UpdateLastSeenAt(int timestamp)
        {
            return null;
        }

        // TODO: Implement UpdateLastSeenAt
        private User UpdateLastSeenAt()
        {
            return null;
        }

        // TODO: Implement IncrementUserSession
        private User IncrementUserSession(int count = 1)
        {
            return null;
        }

        // TODO: Implement DeleteUserCompany
        private User DeleteUserCompany(String userId, String companyId)
        {
            return null;
        }
    }
}