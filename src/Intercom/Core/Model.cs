using System;
using Intercom.Core;
using Intercom.Data;
using Intercom.Clients;
using Intercom.Exceptions;
 
namespace Intercom.Core
{
	public class Model
	{
		public virtual string id { set; get; }
		public virtual string type { get; set; }

		public Model ()
		{
		}
	}
}