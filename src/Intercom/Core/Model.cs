using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;
 

namespace Library.Core
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