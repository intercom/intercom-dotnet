using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections.Generic;

namespace Intercom.Data
{
    public class UserCount
    {
        public class UserCountEntry {
            public int count { set; get; }
            public String name { set; get; }
            public String remote_company_id { set; get; }
        }

        public List<UserCountEntry> counts = new List<UserCountEntry>();

        public UserCount()
        {
        }
    }
}