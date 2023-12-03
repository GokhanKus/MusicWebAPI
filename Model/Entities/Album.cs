using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
	public class Album:BaseEntity
	{		
		public string AlbumName { get; set; }
		public List<Song> Songs { get; set; }
	}
}
