using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.ArtistDTO
{
	public class ArtistUpdateDTO
	{
        public int Id { get; set; }
        public string? ArtistName{ get; set; }
        public string? Nationality{ get; set; }
    }
}
