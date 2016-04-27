using System;
using System.Collections.Generic;

namespace Library
{
    public class UserCount
    {
        public class UserCountEntry {
            public String name { set; get; }
            public int count { set; get; }
            public String remote_company_id { set; get; }
        }

        public List<UserCountEntry> counts = new List<UserCountEntry>();

        public UserCount()
        {
        }
    }
}