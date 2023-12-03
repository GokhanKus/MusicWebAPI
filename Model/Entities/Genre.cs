using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
	public class Genre
	{
		public int GenreId { get; set; } //classismiyle aynı sonunda Id var primary key olur otomatik
		public string GenreName { get; set; }
		public List<Song> Songs { get; set; } //navigation property

	}
}
