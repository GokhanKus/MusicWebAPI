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
	public class SongRepository : GenericRepository<Song, SongContext>, ISongRepository
	{
		public SongRepository(SongContext tcontext) : base(tcontext)
		{

		}
	}
}
