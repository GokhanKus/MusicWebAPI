using Infrastructure.Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
		public string? SongName { get; set; }
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		public Language Language { get; set; }

		public int? AlbumId { get; set; }   //burayı nullable yapmazsak, default olarak 0 degerini alır ve album tablosunda albumId'si 0 olan kayıta eklemeye calisir oyle kayit olmadigi icin hata verir
		public List<Genre>? Genres { get; set; } //many to many relation with Genre
		public Album? Album { get; set; } //one to many relation with Album ( burada many to many de yapılabilir )
			
		//bir albumde birden fazla şarkı olabilir, bir şarkı da birden fazla albumde bulunabilir(aynı şarkıyı farklı biri de söylemis olabilir.)
		public List<Artist>? Artists { get; set; } //many to many bir şarkıyı birden fazla sanatçı soyler, bir sanatçı da birden fazla şarkı soyler
		public int? ReleaseDate { get; set; }

		public DateTime ModifiedDate { get; set; }


	}
}
