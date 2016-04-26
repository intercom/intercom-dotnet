using System;
using System.Collections.Generic;

namespace Library
{
    public class ConversationMessage : Message
    {
        public string subject { get; set; }
        public Author author { get; set; }
        public List<Attachment> attachments { get; set; }
        public object url { get; set; }
    }
}

