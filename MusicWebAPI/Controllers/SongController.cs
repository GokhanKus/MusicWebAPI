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
		    //ID'si Gelen Artistleri Alacağız.
			var artists = _artistRepository.GetAll(x=>model.ArtistIds.Contains(x.Id)).ToList();

			//Bu Şekilde de Alabiliriz.
			//var artists = new List<Artist>();
			//foreach(int ID in model.ArtistIds)
			//{
			//	var value = _artistRepository.Get(x => x.Id == ID);
			//	artists.Add(value);

			//}


			var song = new Song
			{
				SongName = model.SongName,
				Description = model.Description,
				ReleaseDate = model.ReleaseDate,
				AlbumId = model.AlbumId,
				Artists = artists
			};	
			
			_songRepository.Insert(song);
			return Ok(song);

		}
		[HttpPut]
		public IActionResult UpdateSong(int id, SongUpdateDTO model)
		{
			if (id != model.Id)
			{
				return BadRequest();
			}
			var song = _songRepository.Get(s => s.Id == id);
			if (song == null)
			{
				return NotFound(); //aranan sarki bulunamadi
			}
			song.SongName = model.SongName;
			song.Description = model.Description;
			song.ReleaseDate = model.ReleaseDate;
			song.Artists = new List<Artist> { new Artist { ArtistName = model.ArtistName } }; //guncelleme isleminde ornegin adi x olan artist dbde var iken yine x olarak guncellersek yeni kayit ekliyor bunu duzelt.
			song.Language = model.Language;
			song.ModifiedDate = model.ModifiedDate;
			try
			{
				_songRepository.Update(song);
				return Ok(new { Message = "Başarıyla güncellendi" });
			}
			catch (Exception ex)
			{
				return BadRequest(new { ErrorMessage = $"Güncelleme hatası: {ex.Message}" });
			}
		}
		[HttpDelete]
		public IActionResult DeleteSong(int id)
		{
			var song = _songRepository.Get(s => s.Id == id);
			if (song == null)
			{
				return NotFound(new { Message = "silinecek sarki bulunamadi" });
			}
			_songRepository.Delete(song);
			return NoContent(); //basarili bir silme isleminden sonra 204 durum kodu döndürülür
		}
	}
}
