using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.SongDTO
{
	public class SongInsertDTO
	{
		public string SongName { get; set; } = null!;
        public string? Description{ get; set; }
		public string? ArtistName { get; set; }
		public int? ReleaseDate { get; set; }
		//song eklerken albumId'yide atamamız lazım ya da esnek hale getirmemiz lazım yoksa foreign key constraint hatası alıyoruz ONEMLI SongController AddSong fonkiyonuna bak
		public int? AlbumId { get; set; }

	}
}
