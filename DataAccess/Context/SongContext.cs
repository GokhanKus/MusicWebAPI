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

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Artist>().Property(b => b.ArtistName).IsRequired(); //movie.cs'teki Title bilgisi required yaptık
			modelBuilder.Entity<Artist>().Property(b => b.ArtistName).HasMaxLength(100); //max 500 char  

			modelBuilder.Entity<Song>().Property(b => b.SongName).IsRequired();
			modelBuilder.Entity<Song>().Property(b => b.SongName).HasMaxLength(75);
		}

		public DbSet<Artist> Artists { get; set; }
		public DbSet<Song> Songs { get; set; }
		public DbSet<Album> Albums { get; set; }
		public DbSet<Genre> Genres { get; set; }

	}
}
