using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs.ArtistDTO
{
	public class ArtistInsertDto
	{
		public string ArtistName { get; set; } = null!;
		public Region Region{ get; set; }

	}
}
