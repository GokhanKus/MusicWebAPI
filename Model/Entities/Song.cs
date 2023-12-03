using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
	/// <summary>
	/// This is A Song Entity
	/// </summary>
	public class Song:BaseEntity
	{
		public int SongId { get; set; }
		public string SongName { get; set; }
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		public string? Language { get; set; }
		public List<Genre> Genres { get; set; } //many to many relation with Genre
		public List<Album> Albums { get; set; } //many to many relation with Album ( burada one to many de yapılabilir )
												//bir albumde birden fazla şarkı olabilir, bir şarkı da birden fazla albumde bulunabilir(aynı şarkıyı farklı biri de söylemis olabilir.)
		public List<Artist> Artists { get; set; } //many to many bir şarkıyı birden fazla sanatçı soyler, bir sanatçı da birden fazla şarkı soyler
		public int? ReleaseDate { get; set; }


	}
}
