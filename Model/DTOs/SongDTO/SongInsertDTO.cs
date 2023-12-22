﻿using Model.Entities;
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
		//song eklerken albumId'yide atamamız lazım (one to many relation) yoksa foreign key constraint hatası alıyoruz ONEMLI
		public int AlbumId { get; set; }
        
    }
}
