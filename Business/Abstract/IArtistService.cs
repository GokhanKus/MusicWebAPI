using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface IArtistService
	{
		public int Delete(Artist artist);
		public Artist Get(Expression<Func<Artist, bool>> expression = null, params string[] includeList);
		public IEnumerable<Artist> GetAll(Expression<Func<Artist,bool>> expression = null, params string[] includeList);
		public Artist Insert(Artist artist);
		public int Update(Artist artist);
		public void SoftDelete(int Id);
		public bool ExistingArtist(Artist model);

    }
}
