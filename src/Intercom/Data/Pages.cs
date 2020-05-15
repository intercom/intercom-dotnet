using Intercom.Core;

namespace Intercom.Data
{
	public class Pages : Model
	{
		public Next next { get; set; }
		public int page { get; set; }
		public int per_page { get; set; }
		public int total_pages { get; set; }
	}
}