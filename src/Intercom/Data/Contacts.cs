using Intercom.Core;
using System.Collections.Generic;

namespace Intercom.Data
{
    public class Contacts : Models
    {
        public List<Contact> contacts { set; get; }
        public string scroll_param { get; set; }

        public Contacts()
        {
        }
    }
}