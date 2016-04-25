using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    public class Author : Model
    {
    }

    public class ConversationMessage : Message
    {
        public string subject { get; set; }
        public Author author { get; set; }
        public List<Attachment> attachments { get; set; }
        public object url { get; set; }
    }

    public class Assignee : Model
    {
    }

    public class Attachment: Model
    {
        public string name { get; set; }
        public string url { get; set; }
        public string content_type { get; set; }
        public int filesize { get; set; }
        public object width { get; set; }
        public object height { get; set; }
    }

    public class ConversationPart : Model
    {
        public string part_type { get; set; }
        public string body { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public int notified_at { get; set; }
        public object assigned_to { get; set; }
        public Author author { get; set; }
        public List<Attachment> attachments { get; set; }
    }

    public class Conversation : Model
    {
        public int created_at { get; set; }
        public int updated_at { get; set; }
        public Assignee assignee { get; set; }
        public User user { get; set; }
        public bool open { get; set; }
        public bool read { get; set; }
        public ConversationMessage conversation_message { get; set; }
        [JsonConverter(typeof(ListJsonConverter))]
        public List<Tags> tags { get; set; }
        public List<ConversationPart> conversation_parts { get; set; }
    }
}