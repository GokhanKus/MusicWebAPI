using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
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
			return _artistRepository.Insert(artist);
		}

		public int Update(Artist artist)
		{
			return _artistRepository.Update(artist);
		}
		public void SoftDelete(int Id)
		{
			//throw new NotImplementedException();
		}
	}
}
