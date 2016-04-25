using System;
using System.Collections.Generic;

namespace Library
{
    public class AdminConversationReply : Reply
    {
        public string type
        {
            get
            {
                return Reply.ReplySenderType.ADMIN;
            }
        }

        public String admin_id { set; get; }

        public String assignee_id { set; get; }

        public AdminConversationReply(String conversationId,
                                      String adminId, 
                                      String messageType = Reply.ReplyMessageType.COMMENT,
                                      String body = "",
                                      List<String> attachementUrls = null)
            : base(conversationId, Reply.ReplyMessageType.COMMENT, body, attachementUrls)
        {

            if (String.IsNullOrEmpty(conversationId))
                throw new ArgumentNullException("conversation_id is null or empty.");

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