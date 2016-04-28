using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;

namespace Library.Data
{
    public class TagCount
    {
        public Dictionary<String, int> tags = new Dictionary<string, int>();

        public TagCount()
        {
        }
    }
}