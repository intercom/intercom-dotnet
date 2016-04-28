using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using System.Collections.Generic;
using Newtonsoft.Json;
using Library.Converters.ClassConverters;

namespace Library.Data
{
    [JsonConverter(typeof(UserCountJsonConverter))]
    public class UserTagCount : TagCount
    {
    }
}