using DataAccess.Abstract;
using Infrastructure.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.DTOs.SongDTO;
using Model.Entities;

namespace MusicWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SongController : ControllerBase
	{
		private readonly ISongRepository _songRepository;
		private readonly IArtistRepository _artistRepository;
		public SongController(ISongRepository songRepository, IArtistRepository artistRepository)
		{
			_songRepository = songRepository;
			_artistRepository = artistRepository;
		}
		[HttpGet]
		public IActionResult GetAllSongs()
		{
			var songs = _songRepository.GetAll();
			return Ok(songs);

		}
		[HttpGet("{Id}")]
		public IActionResult GetSong(int Id)
		{
			var song = _songRepository.Get(s => s.Id == Id);//,includeList:"Album"
			if (song != null)
			{
				return Ok(song);
			}
			return BadRequest("id degeri hatali");
		}
		[HttpPost]
		public IActionResult AddSong(SongInsertDTO model)
		{
			var song = new Song
			{
				SongName = model.SongName,
				Description = model.Description,
				ReleaseDate = model.ReleaseDate,
			};

			#region Foreign Key Constraint Failed Hatasi

			/*
			onceden song entityde AlbumId default olarak 0 geliyordu ve albumId'nin degerini belirtmezsek bu hataya sebep oluyordu, cunku sarkiyi
			albumId'si 0 olan kayıda eklemeye calisiyordu ve oyle bir kayit olmadigi icin FOREIGN KEY CONSTRAINT FAILED hatası alıyorduk
			biz bunu esnek hale getirdik asagida kontrol ederek ve nullable yaptik yani artik sarkiyi eklerken bir albume ait olmak zorunda degil.
			 */
			#endregion
			var existingAlbum = _songRepository.Get(i => i.AlbumId == model.AlbumId);
			if (existingAlbum != null)
			{
				song.AlbumId = model.AlbumId;
			}

			var existingArtist = _artistRepository.Get(i => i.ArtistName == model.ArtistName);
			if (existingArtist == null)
			{
				existingArtist = new Artist
				{
					ArtistName = model.ArtistName??""
				};
				_artistRepository.Insert(existingArtist);
			}

			//song.Artists.Add(existingArtist);

			_songRepository.Insert(song);
			return Ok(song);

		}
	}
}
