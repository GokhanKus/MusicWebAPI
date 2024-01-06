using Business.Abstract;
using Business.Concrete;
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
		//private readonly ISongRepository _songRepository;
		//private readonly IArtistRepository _artistRepository;
		private readonly ISongService _songService;
		private readonly IArtistService _artistService;
		public SongController(ISongService songService, IArtistService artistService)
		{
			_songService = songService;
			_artistService = artistService;
		}

		[HttpGet]
		public IActionResult GetAllSongs()
		{
			var songs = _songService.GetAll();
			return Ok(songs);

		}
		[HttpGet("{Id}")]
		public IActionResult GetSong(int Id)
		{
			var song = _songService.Get(s => s.Id == Id);//,includeList:"Album"
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
			var existingAlbum = _songService.Get(i => i.AlbumId == model.AlbumId);
			if (existingAlbum != null)
			{
				song.AlbumId = model.AlbumId;
			}

			var existingArtist = _artistService.Get(i => i.ArtistName == model.ArtistName); //burayı duzenle cunku artık artist ve song arasında one to many yaptık
			if (existingArtist == null)
			{
				existingArtist = new Artist
				{
					ArtistName = model.ArtistName ?? ""
				};
				_artistService.Insert(existingArtist);
			}

			//song.Artists.Add(existingArtist);

			_songService.Insert(song);
			return Ok(song);

		}
		[HttpPut]
		public IActionResult UpdateSong(/*int Id, */SongUpdateDTO model)
		{
			//if (id != model.Id)
			//{
			//	return BadRequest();
			//}
			//var song = _songService.Get(s => s.Id == Id);

			var song = _songService.Get(s => s.Id == model.Id);
			if (song == null)
			{
				return NotFound(); //aranan sarki bulunamadi
			}
			song.SongName = model.SongName;
			song.Description = model.Description;
			song.ReleaseDate = model.ReleaseDate;
			song.Artist = new Artist { ArtistName = model.ArtistName }; //guncelleme isleminde ornegin adi x olan artist dbde var iken yine x olarak guncellersek yeni kayit ekliyor bunu duzelt.
			song.Language = model.Language;
			song.ModifiedDate = model.ModifiedDate;
			try
			{
				_songService.Update(song);
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
			var song = _songService.Get(s => s.Id == id);
			if (song == null)
			{
				return NotFound(new { Message = "silinecek sarki bulunamadi" });
			}
			_songService.Delete(song);
			return NoContent(); //basarili bir silme isleminden sonra 204 durum kodu döndürülür
		}
	}
}
