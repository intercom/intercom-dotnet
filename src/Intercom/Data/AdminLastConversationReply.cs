using System;
namespace Intercom.Data
{
    public class AdminLastConversationReply
    {
        public string admin_id { get; set; }
        public string message_type { get; set; }
        public string body { get; set; }
        public string intercom_user_id { get; set; }
        public string type { get; set; }

        public AdminLastConversationReply()
        {
        }
    }
}
