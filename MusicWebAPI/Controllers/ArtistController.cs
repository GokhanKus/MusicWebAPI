using DataAccess.Abstract;
using DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.DTOs.ArtistDTO;
using Model.Entities;

namespace MusicWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArtistController : ControllerBase
	{
		private readonly IArtistRepository _artistRepository;

		public ArtistController(IArtistRepository artistRepository)
		{
			_artistRepository = artistRepository;
		}
		[HttpGet]
		public IActionResult GetAllArtist()
		{
			var value = _artistRepository.GetAll();

			return Ok(value);
		}
		[HttpGet("{Id}")]
		public IActionResult GetArtist(int Id) //songların gelmemesi icin dto yazılabilir
		{
			//_artistRepository.Get
			//_context.Artist.FirstOrDefault(i=>i.id == model.id)

			var artist = _artistRepository.Get(i => i.Id == Id);

			if (artist == null)
			{
				return BadRequest("id degeri hatali");
			}
			return Ok(artist);
		}

		[HttpPost]
		public IActionResult AddArtist(ArtistInsertDto artistDto)
		{
			var artist = new Artist
			{
				ArtistName = artistDto.ArtistName,
				Nationality = artistDto.Nationality,
			};

			_artistRepository.Insert(artist);

			return Ok(artist);
		}

		[HttpPut]
		public IActionResult UpdateArtist(ArtistUpdateDTO artist)
		{
			var model = _artistRepository.Get(x => x.Id == artist.Id);
			if (model != null)
			{
				model.ArtistName = artist.ArtistName;
				model.Nationality = artist.Nationality;
				_artistRepository.Update(model);
				return Ok(model);
			}
			return BadRequest("entity bulunamadi");
		}

		[HttpDelete]
		public IActionResult DeleteArtist(int Id)
		{
			var entity = _artistRepository.Get(i => i.Id == Id);

			if (entity != null)
			{
				_artistRepository.Delete(entity);
				return Ok(entity);
			}
			return BadRequest("entity bulunamadi ve silinemedi");
		}
	}
}
