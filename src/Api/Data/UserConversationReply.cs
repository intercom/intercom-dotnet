using System;
using System.Collections.Generic;

namespace Library
{
    public class UserConversationReply : Reply
    {
        public string type
        {
            get
            {
                return Reply.ReplySenderType.USER;
            }
        }

        public String message_type
        { 
            get
            { 
                return Reply.ReplyMessageType.COMMENT; 
            }
        }

        public String email { set; get; }

        public String user_id { set; get; }

        public String intercom_user_id { set; get; }

        public UserConversationReply(
            String body,
            String intercomUserId = null, 
            String email = null, 
            String userId = null,
            List<String> attachementUrls = null)
            : base(Reply.ReplyMessageType.COMMENT, body, attachementUrls)
        {
            if (String.IsNullOrEmpty(id) && String.IsNullOrEmpty(email) && String.IsNullOrEmpty(user_id))
                throw new ArgumentException("you need to provide either 'id', 'user_id', 'email' to view a user.");

            this.email = email;
            this.user_id = userId;
            this.intercom_user_id = intercomUserId;
        }
    }
}
