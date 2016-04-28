using System;
using Library.Core;
using Library.Data;


using Library.Clients;

using Library.Exceptions;


namespace Library.Data
{
	public class Segment : Model
	{
        public string name { get; set; }
        public int created_at { get; set; }
        public int updated_at { get; set; }

		public Segment ()
		{
		}
	}
}

