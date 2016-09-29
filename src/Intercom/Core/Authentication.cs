using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;

namespace Intercom.Core
{
	public class Authentication
	{
        public String AppId { private set; get;}
		public String AppKey{ private set; get;}
        public String PersonalAccessToken { private set; get; }

        public Authentication (String personalAccessToken)
        {
            if (String.IsNullOrEmpty (personalAccessToken))
                throw new ArgumentException ("'PersonalAccessToken' argument is not found.");

            this.PersonalAccessToken = personalAccessToken;
        }

        public Authentication (String appId, String appKey)
        {
            if (String.IsNullOrEmpty (appId))
                throw new ArgumentException ("'appId' argument is not found.");

            if (String.IsNullOrEmpty (appKey))
                throw new ArgumentException ("'appKey' argument is not found.");

            this.AppId = appId;
            this.AppKey = appKey;
        }
	}
}