using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;

namespace Library.Data
{
    public class ConversationMessage : Message
    {
        public string subject { get; set; }
        public Author author { get; set; }
        public List<Attachment> attachments { get; set; }
        public object url { get; set; }
    }
}

