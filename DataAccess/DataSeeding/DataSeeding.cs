using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.Entities;
using Model.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataSeeding
{
	public static class DataSeeding
	{
		public static void SeedData(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider.GetService<SongContext>();

			if (context != null)
			{
				if (context.Database.GetPendingMigrations().Any())//olusturulmus ancak uygulanmamis migration varsa;
				{
					context.Database.Migrate();//update-database yapılsın
				}
				if (!context.Albums.Any())  //dbde albums tablosunda hic kayit yoksa;
				{
					context.Albums.AddRange(
						new Album
						{
							AlbumName = "The Great War",
							CreatedDate = DateTime.Now.AddDays(-10),

						}, new Album
						{
							AlbumName = "Fear of the Dark",
							CreatedDate = DateTime.Now.AddDays(-5),
						},
						new Album
						{
							AlbumName = "The Sacrament of Sin",
							CreatedDate = DateTime.Now,
						},
						new Album
						{
							AlbumName = "British Steel",
							CreatedDate = DateTime.Now,
						},
						new Album
						{
							AlbumName = "Gun Ola Harman Ola",
							CreatedDate = DateTime.Now,
						});

					context.SaveChanges();

				}
				if (!context.Songs.Any())
				{
					context.Songs.AddRange(
						new Song
						{
							SongName = "The Red Baron",
							Artists = new List<Artist> { new Artist { ArtistName = "Sabaton", Nationality = "Sweden", CreatedDate = DateTime.Now.AddDays(-30) } },
							CreatedDate = DateTime.Now.AddDays(-30),
							Description = "\"The Red Baron\" by Sabaton is a powerful heavy metal anthem that vividly portrays the legendary World War I flying ace Manfred von Richthofen, known as the Red Baron, capturing the intensity and heroism of aerial combat.",
							Language = Language.English,
							ReleaseDate = 2019,
							Genres = new List<Genre> { new Genre { GenreName = "Power Metal", CreatedDate = DateTime.Now } },
							AlbumId = 1
						},
						new Song
						{
							SongName = "Wasting Love",
							Artists = new List<Artist> { new Artist { ArtistName = "Iron Maiden", Nationality = "The UK", CreatedDate = DateTime.Now.AddDays(-20) } },
							CreatedDate = DateTime.Now.AddDays(-30),
							Description = "Wasting Love by Iron Maiden is a poignant ballad that explores themes of heartbreak and introspection,showcasing the band's versatility beyond their signature heavy metal sound",
							Language = Language.English,
							ReleaseDate = 1992,
							Genres = new List<Genre> { new Genre { GenreName = "Power Metal", CreatedDate = DateTime.Now } },
							AlbumId = 2
						},
						new Song
						{
							SongName = "Killers with the Cross",
							Artists = new List<Artist> { new Artist { ArtistName = "Powerwolf", Nationality = "Germany", CreatedDate = DateTime.Now.AddDays(-50) } },
							CreatedDate = DateTime.Now.AddDays(-30),
							Description = "Killers with the Cross by Powerwolf is a high - energy,anthemic metal track that combines powerful vocals and relentless instrumentals to create a captivating and exhilarating listening experience.",
							Language = Language.English,
							ReleaseDate = 2018,
							Genres = new List<Genre> { new Genre { GenreName = "Power Metal", CreatedDate = DateTime.Now } },
							AlbumId = 3
						});

					context.SaveChanges();

				}
				if (!context.Genres.Any())
				{
					context.Genres.AddRange(
						new Genre
						{
							GenreName = "Heavy Metal",
							CreatedDate = DateTime.Now,
							Songs = new List<Song> {
								new Song
								{
									SongName = "Breaking the Law",
									Artists = new List<Artist> { new Artist { ArtistName = "Judas Priest", Nationality = "Germany", CreatedDate = DateTime.Now.AddDays(-50) } },
									CreatedDate = DateTime.Now.AddDays(-30),
									Description = "Breaking the Law by Judas Priest is a classic heavy metal anthem that rebelliously captures the spirit of defiance, featuring iconic riffs and a memorable chorus.",
									Language = Language.English,
									ReleaseDate = 1980,
									Genres = new List<Genre> { new Genre { GenreName = "Rock Metal", CreatedDate = DateTime.Now } },
									AlbumId = 4
								}},
						});

					context.SaveChanges();

				}
				if (!context.Artists.Any())
				{
					context.Artists.AddRange(
						new Artist
						{
							ArtistName = "Erkin Koray",
							Nationality = "Turkey",
							CreatedDate = DateTime.Now,
							Songs = new List<Song> { new Song
							{
								SongName = "Akrebin Gozleri",
								CreatedDate = DateTime.Now.AddDays(-30),
								Description = "Akrebin Gözleri by Erkin Koray is a psychedelic rock masterpiece that blends innovative guitar work with mystical lyrics, capturing the essence of Turkish rock music in the 1970s.",
								Language = Language.Turkish,
								ReleaseDate = 1996,
								Genres = new List<Genre> { new Genre { GenreName = "psychedelic rock ", CreatedDate = DateTime.Now } },
								AlbumId = 5
							}},
						});

					context.SaveChanges();

				}
			}
		}
	}
}
