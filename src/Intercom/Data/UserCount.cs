using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;

namespace Library.Data
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