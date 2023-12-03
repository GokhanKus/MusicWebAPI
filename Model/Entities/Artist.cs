using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
	public class Artist
	{
		public int ArtistId { get; set; }
		public string ArtistName { get; set; }
		public string? Nationality { get; set; }
		public List<Song> Songs { get; set; }

	}
}
