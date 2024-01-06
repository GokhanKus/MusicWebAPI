
using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.GenreDTO;
using Model.Entities;

namespace MusicWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GenreController : ControllerBase
	{
		//private readonly IGenreRepository _genreRepository;
		//Arık Her şeyi Servis Üzerinden Yapacağız.
		private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            //_genreRepository = genreRepository;
            _genreService = genreService;
        }
        [HttpGet]
		public IActionResult GetGenres()
		{
			var genres = _genreService.GetAll();
			return Ok(genres);
		}
		[HttpGet("{Id}")]
		public IActionResult GetGenre(int Id)
		{
			var genre = _genreService.Get(x=>x.Id==Id);
		
			if (genre == null)
			{
				//return BadRequest();
				return NotFound(new { Message = $"{Id} id'sine sahip bir genre bulunamadi" });
			}
			return Ok(genre);
		}
		[HttpPost]
		public IActionResult AddGenre(GenreInsertDTO model)
		{
			//var existingGenre = _genreRepository.Get(g => g.GenreName == model.GenreName);
			var existingGenre = _genreService.Get(x => x.GenreName == model.GenreName);
            if (existingGenre != null)
			{
				return Conflict(new { Message = "Bu tür zaten mevcut" });
			}
			var genre = new Genre
			{
				GenreName = model.GenreName,
				CreatedDate = DateTime.Now,
			};
			_genreService.Insert(genre);
			//_genreRepository.Insert(genre);
			
			return CreatedAtAction(nameof(GetGenre), new { Id = genre.Id }, genre);

			//bu sekilde return ederek, yeni oluşturulan veriyi URI'sinin, yeni oluşturulan türün ID'si ile GetGenre action'ını çağırarak elde edilebileceğini belirtir.
		}
		[HttpPut]
		public IActionResult UpdateGenre(GenreUpdateDTO model, int? Id)
		{
            //var existingGenre = _genreRepository.Get(g => g.GenreName == model.GenreName);

			

            var existingGenre = _genreService.Get(g => g.GenreName == model.GenreName);

            if (existingGenre != null)
			{
				return Conflict(new { Message = "Bu tür zaten mevcut" });
			}
			if (Id != model.Id)
			{
				return BadRequest($"{Id} id degerine sahip olan veri bulunamadi");
			}
			var genre = _genreService.Get(g => g.Id == model.Id);
			if (genre == null)
			{
				return NotFound(new { Message = "Güncellenmek istenen tür bulunamadı." });
			}
			genre.GenreName = model.GenreName;

			return Ok(genre);
		}
		[HttpDelete]
		public IActionResult DeleteGenre(int? Id)
		{
			if (Id == null)
			{
				return NotFound(new { Message = "boyle bir ıd degerine sahip genre bulunamadı." });
			}
			var genre = _genreService.Get(i => i.Id == Id);
			if (genre != null)
			{
				//_genreRepository.Delete(genre);
				_genreService.Delete(genre);

				return Ok();
				//return NoContent();
			}
			return BadRequest(new { Message = "silinecek genre bulunamadı." });

		}
	}
}
