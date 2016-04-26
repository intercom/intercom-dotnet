using System;

namespace Library
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

