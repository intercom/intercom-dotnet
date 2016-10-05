using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;


namespace Intercom.Data
{
    public class UserConversationMessage : Message
	{

		public class From : Model
		{
			public String email { set; get; }
			public String user_id { set; get; }

            public From (String type = Message.MessageFromOrToType.USER, String id = null, String email = null, String user_id = null)
			{
				if (String.IsNullOrEmpty (type))
					throw new ArgumentNullException (nameof(type));

				if (String.IsNullOrEmpty (id) && String.IsNullOrEmpty (email) && String.IsNullOrEmpty (user_id))
					throw new ArgumentException ("you need to provide either 'id', 'user_id', 'email' to view a user.");

                if (type != Message.MessageFromOrToType.USER && type != Message.MessageFromOrToType.CONTACT)
					throw new ArgumentException ("'type' value must be either 'contact' or 'user'.");

				this.id = id;
				this.email = email;
				this.user_id = user_id;
				this.type = type;
			}
		}

		public From from { set; get; }

        public UserConversationMessage (From from, String body)
		{
			this.from = from;
			this.body = body;
		}
	}
}