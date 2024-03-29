﻿using Infrastructure.Model;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities
{
	public class Artist:BaseEntity
	{
		public string? ArtistName { get; set; } = string.Empty;
		public Region Region{ get; set; }
		public List<Song>? Songs { get; set; }
		public DateTime ModifiedDate { get; set; }

	}
}
