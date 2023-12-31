using Infrastructure.Model;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IGenreService
    {
        int Delete(Genre entity);

        public Genre Get(Expression<Func<Genre, bool>> expression, params string[] IncludeLists);

        public IEnumerable<Genre> GetAll(Expression<Func<Genre, bool>> expression = null, params string[] IncludeList);    

        public Genre Insert(Genre entity);

        public int Update(Genre entity);

        //public void SoftDelete(int Id);

    }
}
