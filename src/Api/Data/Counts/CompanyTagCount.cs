using System;
using Newtonsoft.Json;

namespace Library
{
    [JsonConverter(typeof(CompanyCountJsonConverter))]
    public class CompanyTagCount : TagCount
    {
        public CompanyTagCount()
        {
        }
    }
}

