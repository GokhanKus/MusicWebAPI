using Model.Entities;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.SongDTO
{
	public class SongUpdateDTO
	{
        public int Id { get; set; }
        public string SongName { get; set; } = null!;
        public string? Description{ get; set; }
		public string? ArtistName { get; set; }
		public Language Language{ get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int? ReleaseDate { get; set; }
    }
}
