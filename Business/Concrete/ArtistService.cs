using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Model.DTOs.ArtistDTO;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistService(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        public int Delete(Artist artist)
        {
            return _artistRepository.Delete(artist);
        }

        public Artist Get(Expression<Func<Artist, bool>> expression = null, params string[] includeList)
        {
            return _artistRepository.Get(expression, includeList);
        }

        public IEnumerable<Artist> GetAll(Expression<Func<Artist, bool>> expression = null, params string[] includeList)
        {
            return _artistRepository.GetAll(expression, includeList);
        }

        public Artist Insert(Artist artist)
        {
            UpperArtistName(artist);
            return _artistRepository.Insert(artist);
        }

        public int Update(Artist artist)
        {
            UpperArtistName(artist);
            return _artistRepository.Update(artist);
        }

        public void SoftDelete(int Id)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Artist Name'in ilk Harfleri büyük yazan metot.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private void UpperArtistName(Artist model)
        {
            string Name = model.ArtistName;
            Name = Name.Trim();

            Name = Name.ToLower();

            char[] charArray = Name.ToCharArray();

            charArray[0] = Char.ToUpper(charArray[0]);

            for (int i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] == ' ')
                {
                    charArray[i + 1] = Char.ToUpper(charArray[i + 1]);
                }

            }

            string newName = new string(charArray);

            model.ArtistName = newName;

        }
        public bool ExistingArtist(Artist model)
        {
            var artists = GetAll();
            string artistName = model.ArtistName;
            //artistName.Replace(" ", "");

            //foreach (var artist in artists)
            //{
            //	artist.ArtistName.Replace(" ", "");
            //	if (model.ArtistName == artist.ArtistName)
            //	{
            //		return false;
            //	}
            //         }
            var matchingArtists = artists.FirstOrDefault(a => a.ArtistName == model.ArtistName);
            if (matchingArtists == null)
            {
                return true;
            }

            return false;
        }
    }
}
