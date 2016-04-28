using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using Newtonsoft.Json;
using Library.Converters.ClassConverters;

namespace Library.Data
{
    [JsonConverter(typeof(CompanyCountJsonConverter))]
    public class CompanySegmentCount : SegmentCount
    {
        public CompanySegmentCount()
        {
        }
    }
}

