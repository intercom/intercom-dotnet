using System;
using Newtonsoft.Json;

namespace Library
{
    [JsonConverter(typeof(CompanyCountJsonConverter))]
    public class CompanySegmentCount : SegmentCount
    {
        public CompanySegmentCount()
        {
        }
    }
}

