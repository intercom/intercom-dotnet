using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;


namespace Intercom.Data
{
    public class AdminConversationMessage : Message
	{

        public class From { 
			public String id { set; get; }
			public String type { private set; get; }

			public From(String id)
			{
				if(String.IsNullOrEmpty(id))
					throw new ArgumentNullException (nameof(id));
				
				this.id = id;
                this.type = Message.MessageFromOrToType.ADMIN;
			}
		}

		public class To { 
			public String id { set; get; }
			public String email { set; get; }
			public String user_id { set; get; }
			public String type { set; get; }

            public To(String type = Message.MessageFromOrToType.USER,  String id = null, String email = null, String user_id = null)
			{
				if(String.IsNullOrEmpty(type))
					throw new ArgumentNullException (nameof(type));

				if(String.IsNullOrEmpty(id) && String.IsNullOrEmpty(email) && String.IsNullOrEmpty(user_id))
					throw new ArgumentException ("you need to provide either 'id', 'user_id', 'email' to view a user.");

                if(type != Message.MessageFromOrToType.USER && type != Message.MessageFromOrToType.CONTACT)
					throw new ArgumentException ("'type' vale must be either 'contact' or 'user'.");

				this.id = id;
				this.email = email;
				this.user_id = user_id;
				this.type = type;
			}
		}

		public string message_type { get; set; }
		public string subject { get; set; }
		public string template { get; set; }
		public From from { get; set; }
		public To to { get; set; }

        public AdminConversationMessage (
            AdminConversationMessage.From from, 
            AdminConversationMessage.To to,
            String message_type = Message.MessageType.EMAIL,
            String template = Message.MessageTemplate.PLAIN,
			String subject = "", 
			String body = "")
		{
			this.to = to;
			this.from = from;
			this.message_type = message_type;
			this.template = template;
			this.subject = subject;
			this.body = body;
		}
	}
}
