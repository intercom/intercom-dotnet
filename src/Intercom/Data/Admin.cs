using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;


namespace Intercom.Data
{
    public class Admin : Model
    {
        public string name { get; set; }
        public string email { get; set; }

        public Admin()
        {
        }
    }
}