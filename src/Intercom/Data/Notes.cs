using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;

namespace Library.Data
{
    public class Notes : Models
    {
        public List<Note> notes { set; get; }

        public Notes()
        {
        }
    }
}