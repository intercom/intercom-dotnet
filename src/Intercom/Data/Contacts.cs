using Intercom.Core;
using System.Collections.Generic;

namespace Intercom.Data
{
    public class Contacts : Models
    {
        public List<Contact> data { set; get; }
        public string scroll_param { get; set; }

        public Contacts()
        {
        }

        public Contact FindExact(string searchString)
        {
            Contact result = new Contact();

            foreach (Contact contact in data)
            {
                if (contact.id == searchString)
                {
                    result = contact;
                }
                else if (contact.email == searchString)
                {
                    result = contact;
                }
                else if (contact.user_id == searchString)
                {
                    result = contact;
                }
                else
                    result = null;
            }

            return result;
        }
    }
}