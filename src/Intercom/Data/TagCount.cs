using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections.Generic;

namespace Intercom.Data
{
    public class TagCount
    {
        public Dictionary<String, int> tags = new Dictionary<string, int>();

        public TagCount()
        {
        }
    }
}