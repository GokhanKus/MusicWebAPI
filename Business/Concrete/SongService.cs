using Business.Abstract;
using DataAccess.Abstract;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class SongService : ISongService
	{
		private readonly ISongRepository _songRepository;
		public SongService(ISongRepository songRepository)
		{
			_songRepository = songRepository;
		}

		public int Delete(Song song)
		{
			return _songRepository.Delete(song);
		}

		public Song Get(Expression<Func<Song, bool>> expression, params string[] includeList)
		{
			return _songRepository.Get(expression, includeList);
		}

		public IEnumerable<Song> GetAll(Expression<Func<Song, bool>> expression, params string[] includeList)
		{
			return _songRepository.GetAll(expression, includeList);
		}

		public Song Insert(Song song)
		{
			return _songRepository.Insert(song);
		}

		public int Update(Song song)
		{
			return _songRepository.Update(song);
		}
		//public void SoftDelete(int Id) { }
		
	}
}
