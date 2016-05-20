using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using System.Collections.Generic;

namespace Intercom.Data
{
    public class Notes : Models
    {
        public List<Note> notes { set; get; }

        public Notes()
        {
        }
    }
}