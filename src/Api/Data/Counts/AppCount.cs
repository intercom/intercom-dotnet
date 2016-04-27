using System;
using Newtonsoft.Json;

namespace Library
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