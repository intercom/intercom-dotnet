using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections.Generic;

namespace Intercom.Data
{
    public class AdminConversationReply : Reply
    {
        public override string type
        {
            get
            {
                return Reply.ReplySenderType.ADMIN;
            }
        }

        public String admin_id { set; get; }
        public String assignee_id { set; get; }
        public String intercom_user_id { get; set;  }

        public AdminConversationReply(String adminId,
                                      String messageType = Reply.ReplyMessageType.COMMENT,
                                      String body = "",
                                      String intercom_user_id = null,
                                      String conversationId = null,
                                      List<String> attachementUrls = null)
            : base(conversationId, messageType, body, attachementUrls)
        {

            if (String.IsNullOrEmpty(conversationId) && !String.IsNullOrEmpty(intercom_user_id))
                throw new ArgumentNullException(nameof(conversationId));

            if ((messageType == Reply.ReplyMessageType.COMMENT ||
                messageType == Reply.ReplyMessageType.NOTE) &&
                String.IsNullOrEmpty(body))
            {
                throw new ArgumentException("'body' argument shouldnt be empty when the message type is 'comment' or 'note'.");
            }

            this.admin_id = adminId;
        }
    }
}