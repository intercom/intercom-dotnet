using System;

namespace Library.Core
{
	public class Authentication
	{
		public String AppId { private set; get;}
		public String AppKey{ private set; get;}

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

