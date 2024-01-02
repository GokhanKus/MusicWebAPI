using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }
        public int Delete(Genre entity)
        {
            return _genreRepository.Delete(entity);
            
        }

        public IEnumerable<Genre> GetAll(Expression<Func<Genre, bool>> expression = null, params string[] IncludeList)
        {
            return _genreRepository.GetAll(expression, IncludeList);           
        }
      

        public Genre Get(Expression<Func<Genre, bool>> expression = null, params string[] IncludeLists)
        {
            return _genreRepository.Get(expression, IncludeLists);
        }

        public Genre Insert(Genre entity)
        {
            return _genreRepository.Insert(entity);
        }

        //public void SoftDelete(int Id)
        //{
        //    throw new NotImplementedException();
        //}

        public int Update(Genre entity)
        {
            return _genreRepository.Update(entity);
        }
    }
}
