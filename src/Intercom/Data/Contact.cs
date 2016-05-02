using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using Newtonsoft.Json;
using System.Collections.Generic;
using Intercom.Converters.AttributeConverters;

namespace Intercom.Data
{
    public class Contact : User
    {
        public string app_id { get; set; }
        public object remote_created_at { get; set; }
    }
}