using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;

namespace Library.Data
{
    public class Conversations : Models
    {
        public List<Conversation> conversations { set; get; }

        public Conversations()
        {
        }
    }
}

