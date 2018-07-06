using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections.Generic;

namespace Intercom.Data
{
    public class Reply : Model
    {
        public static class ReplyMessageType
        {
            public const String ASSIGNMENT = "assignment";
            public const String COMMENT = "comment";
            public const String CLOSE = "close";
            public const String OPEN = "open";
            public const String NOTE = "note";
        }

        public static class ReplySenderType
        {
            public const String USER = "user";
            public const String ADMIN = "admin";
        }

        public virtual String conversation_id { set; get; }
        public virtual String message_type { set; get; }
        public virtual String body { set; get; }
        public virtual List<String> attachment_urls { get; set; }

        public Reply(
            String conversation_id,
            String messageType = Reply.ReplyMessageType.COMMENT,
            String body = "",
            List<String> attachmentUrls = null)
        {
            if (attachmentUrls != null && attachmentUrls.Count > 5)
            {
                throw new ArgumentException("'attachment_urls' need to be equal or less than 5 urls.");
            }

            this.body = body;
            this.conversation_id = conversation_id;
            this.message_type = messageType;
            this.attachment_urls = attachmentUrls;
        }
    }
}

