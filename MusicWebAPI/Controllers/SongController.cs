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
			var song = _songRepository.Get(s => s.Id == Id);
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
