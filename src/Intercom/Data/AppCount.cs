using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;

using Newtonsoft.Json;
using Library.Converters.AttributeConverters;

namespace Library.Data
{
    public class AppCount : Count
    {
        [JsonConverter(typeof(AppCountJsonConverter))]
        public int company { set; get; }
        [JsonConverter(typeof(AppCountJsonConverter))]
        public int segment { set; get; }
        [JsonConverter(typeof(AppCountJsonConverter))]
        public int user { set; get; }
        [JsonConverter(typeof(AppCountJsonConverter))]
        public int tag { set; get; }
        [JsonConverter(typeof(AppCountJsonConverter))]
        public int lead { set; get; }

        public AppCount()
        {
        }
    }
}