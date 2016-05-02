using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
using Newtonsoft.Json;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    [JsonConverter(typeof(AppCountJsonConverter))]
    public class AppCount : Count
    {
        public int company { set; get; }
        public int segment { set; get; }
        public int user { set; get; }
        public int tag { set; get; }
        public int lead { set; get; }

        public AppCount()
        {
        }
    }
}