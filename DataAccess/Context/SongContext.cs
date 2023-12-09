using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
	public class SongContext:DbContext
	{
		public SongContext(DbContextOptions<SongContext> options) : base(options)
		{

		}

		public DbSet<Artist> Artists { get; set; }
		public DbSet<Song> Songs { get; set; }
		public DbSet<Album> Albums { get; set; }
		public DbSet<Genre> Genres { get; set; }

	}
}
