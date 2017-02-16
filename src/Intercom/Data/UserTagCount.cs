using Newtonsoft.Json;
using Intercom.Converters.ClassConverters;

namespace Intercom.Data
{
    [JsonConverter(typeof(UserCountJsonConverter))]
    public class UserTagCount : TagCount
    {
    }
}