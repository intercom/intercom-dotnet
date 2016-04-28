using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;
using Library.Core;
using System.Collections;

namespace Library.Data
{
    public class Admins : Models
    {
        public List<Admin> admins { set; get; }

        public Admins ()
        {
        }
    }
}