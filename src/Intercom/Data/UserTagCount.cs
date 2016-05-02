using System;
using Intercom.Core;
using Intercom.Data;


using Intercom.Clients;

using Intercom.Exceptions;

using System.Collections.Generic;
using Newtonsoft.Json;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    [JsonConverter(typeof(UserCountJsonConverter))]
    public class UserTagCount : TagCount
    {
    }
}