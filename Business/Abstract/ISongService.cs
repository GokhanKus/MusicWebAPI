using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ISongService
	{
		public int Delete(Song song);
		public Song Get(Expression<Func<Song, bool>> expression = null, params string[] includeList);
		public IEnumerable<Song> GetAll(Expression<Func<Song, bool>> expression = null, params string[] includeList);
		public Song Insert(Song song);
		public int Update(Song song);
		//public void SoftDelete(int Id);
	}
}
