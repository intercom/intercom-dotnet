using System;
using System.Collections.Generic;

namespace Library
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

        public virtual String message_type { set; get; }
        public virtual String body { set; get; }
        public virtual List<String> attachment_urls { get; set; }

        public Reply(
            String messageType = Reply.ReplyMessageType.COMMENT,
            String body = "",
            List<String> attachementUrls = null)
        {
            if (attachementUrls != null && attachementUrls.Count > 5)
            {
                throw new ArgumentException("'attachment_urls' need to be equal or less than 5 urls.");
            }

            this.body = body;
            this.message_type = messageType;
            this.attachment_urls = attachementUrls;
        }
    }
}

