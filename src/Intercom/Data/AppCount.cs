using System;
using Library.Core;
using Library.Data;
using Library.Clients;
using Library.Exceptions;
using Newtonsoft.Json;
using Library.Converters.ClassConverters;

namespace Library.Data
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