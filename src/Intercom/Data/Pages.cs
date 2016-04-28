using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;


namespace Library.Data
{
	public class Pages : Model
	{
		public string next { get; set; }
		public int page { get; set; }
		public int per_page { get; set; }
		public int total_pages { get; set; }
	}
}