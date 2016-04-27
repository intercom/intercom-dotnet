using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Library
{
    [JsonConverter(typeof(UserCountJsonConverter))]
    public class UserSegmentCount : SegmentCount
    {
    }
}