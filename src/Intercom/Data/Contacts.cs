using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections.Generic;

namespace Intercom.Data
{
    public class Contacts : Models
    {
        public List<Contact> contacts { set; get; }

        public Contacts()
        {
        }
    }
}