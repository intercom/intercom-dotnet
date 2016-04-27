using System;
using Newtonsoft.Json;

namespace Library
{
    [JsonConverter(typeof(CompanyCountJsonConverter))]
    public class CompanyUserCount : UserCount
    {
        public CompanyUserCount()
        {
        }
    }
}

