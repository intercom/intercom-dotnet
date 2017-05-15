using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;

namespace Intercom.Data
{
    public class Note : Model
    {
        public long? created_at { get; set; }
        public string body { get; set; }
        public Admin author { get; set; }
        public User user { get; set; }

        public Note()
        {
        }
    }
}
