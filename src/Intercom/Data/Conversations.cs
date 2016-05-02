using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections.Generic;

namespace Intercom.Data
{
    public class Conversations : Models
    {
        public List<Conversation> conversations { set; get; }

        public Conversations()
        {
        }
    }
}

