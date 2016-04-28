using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Library.Converters.AttributeConverters;

namespace Library.Data
{
    public class Contact : User
    {
        public string app_id { get; set; }
        public object remote_created_at { get; set; }
    }
}