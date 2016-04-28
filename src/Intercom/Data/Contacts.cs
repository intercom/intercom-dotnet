using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;

namespace Library.Data
{
    public class Contacts : Models
    {
        public List<Contact> contacts { set; get; }

        public Contacts()
        {
        }
    }
}