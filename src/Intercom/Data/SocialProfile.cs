using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;


namespace Library.Data
{
	public class SocialProfile : Model
	{
		public string type { get; set; }
		public string username { get; set; }
		public string url { get; set; }
	}
}

