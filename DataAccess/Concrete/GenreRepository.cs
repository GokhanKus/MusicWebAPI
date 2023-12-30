using DataAccess.Abstract;
using DataAccess.Context;
using Infrastructure.DataAccess.Concrete;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
	public class GenreRepository : GenericRepository<Genre, SongContext>, IGenreRepository
	{
		public GenreRepository(SongContext tcontext) : base(tcontext)
		{

		}
	}
}
