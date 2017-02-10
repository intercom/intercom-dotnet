using Intercom.Core;
using System.Collections.Generic;

namespace Intercom.Data
{
    public class Users : Models
	{
		public List<User> users { set; get; }
        public string scroll_param { get; set; }

		public Users ()
		{
		}
	}
}