using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;


namespace Library.Data
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