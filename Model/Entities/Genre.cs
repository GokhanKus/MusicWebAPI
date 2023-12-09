using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
	public class Genre:BaseEntity
	{
		public string GenreName { get; set; }
		public List<Song> Songs { get; set; } //navigation property

	}
}
