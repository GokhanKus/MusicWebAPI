﻿using DataAccess.Abstract;
using DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Model.DTOs.ArtistDTO;
using Model.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

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
		#region ReferenceHandler.IgnoreCycles ve ReferenceHandler.Preserve 
		//ReferenceHandler.IgnoreCycles ve ReferenceHandler.Preserve arasındaki fark şudur;
		//preserve'de döngüsel referansı alır daha buyuk json ciktisi olabilir outputu daha karmasiktir.
		//ignore cycles döngusel referansları yok sayar boyutu daha kucuktur daha sadedir
		//Yani aynı nesnenin birden çok referansı olduğunda, yalnızca bir referansı kullanır ve diğer referansları atlar
		#endregion
		[HttpGet]
		public IActionResult GetAllArtist() 
		{			 
			var value = _artistRepository.GetAll(includeList:"Songs");
			
			//return Ok(value);			
			return Ok(value);
		}
		[HttpGet("{Id}")]
		public IActionResult GetArtist(int? Id) //songların gelmemesi icin dto yazılabilir
		{
			//_artistRepository.Get
			//_context.Artist.FirstOrDefault(i=>i.id == model.id)
			if (Id == null)
			{
				return NotFound();//return StatusCode(404, "");
			}
			var artist = _artistRepository.Get(i => i.Id == Id, includeList: "Songs");//,includeList:"Songs"

			return Ok(artist);
		}

		[HttpPost]
		public IActionResult AddArtist(ArtistInsertDto model)
		{
			var artist = new Artist
			{
				ArtistName = model.ArtistName,
				Nationality = model.Nationality,
			};

			_artistRepository.Insert(artist);

			//return Ok(artist);
			return CreatedAtAction(nameof(GetArtist), new { id = artist.Id}, artist);//model instead of artist
		}

		[HttpPut]
		public IActionResult UpdateArtist(ArtistUpdateDTO model)
		{
			var artist = _artistRepository.Get(a => a.Id == model.Id);
			if (artist != null)
			{
				artist.ArtistName = model.ArtistName;
				artist.Nationality = model.Nationality;
				artist.ModifiedDate = model.ModifiedDate;
				_artistRepository.Update(artist);
				//return Ok(artist);
				return NoContent();//204 code basarili bir update veya delete isleminden sonra kullanılabilir
			}
			return BadRequest("entity bulunamadi");
		}

		[HttpDelete]
		public IActionResult DeleteArtist(int? Id)
		{
			if (Id == null)
			{
				return NotFound();
			}
			var entity = _artistRepository.Get(i => i.Id == Id);

			if (entity != null)
			{
				_artistRepository.Delete(entity);
				return Ok(entity);
				//return NoContent();
			}
			return BadRequest("entity bulunamadi ve silinemedi");
		}
	}
}
