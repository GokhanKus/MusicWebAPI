using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        //private readonly IArtistRepository _artistRepository; artik iartistservice kullanacagiz
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
            //_artistRepository = artistRepository;
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
            var value = _artistService.GetAll(includeList: "Songs");



            //return Ok(value);

            //var jsonString = JsonSerializer.Serialize(value, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.IgnoreCycles, WriteIndented = true });//pretty printing
            //return Ok(jsonString);
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
            var artist = _artistService.Get(i => i.Id == Id, includeList: "Songs");//,includeList:"Songs"

            //var jsonString = JsonSerializer.Serialize(artist, new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true });//pretty printing

            if (artist == null)
            {
                return BadRequest("id degeri hatali");
            }
            //return Ok(artist);
            return Ok(artist);
        }

        [HttpPost]
        public IActionResult AddArtist(ArtistInsertDto model)
        {

            var artist = new Artist
            {
                ArtistName = model.ArtistName,
                Region = model.Region,
            };

            bool isExist = _artistService.ExistingArtist(artist);

            if (isExist)
            {
                return BadRequest(new { ErrorMessage = "boyle bir artist mevcut" });
            }
            else
            {
                _artistService.Insert(artist);
            }

            //return Ok(artist);
            return CreatedAtAction(nameof(GetArtist), new { id = artist.Id }, artist);//model instead of artist
        }

        [HttpPut]
        public IActionResult UpdateArtist(ArtistUpdateDTO model)
        {
            var artist = _artistService.Get(a => a.Id == model.Id);
            if (artist != null)
            {
                artist.ArtistName = model.ArtistName;
                artist.Region = model.Region;
                artist.ModifiedDate = model.ModifiedDate;
                _artistService.Update(artist);
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
            var entity = _artistService.Get(i => i.Id == Id);

            if (entity != null)
            {
                _artistService.Delete(entity);
                return Ok(entity);
                //return NoContent();
            }
            return BadRequest("entity bulunamadi ve silinemedi");
        }
    }
}
