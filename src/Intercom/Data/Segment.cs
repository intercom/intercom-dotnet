using Intercom.Core;

namespace Intercom.Data
{
    public class Segment : Model
    {
        public string name { get; set; }
        public long created_at { get; set; }
        public long updated_at { get; set; }
        public int? count { get; set; }

        public Segment()
        {
        }
    }
}

