using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Abstract
{
	public interface IGenericRepository<TEntity>
	{
		IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, params string[] includeList);

		TEntity Get(Expression<Func<TEntity, bool>> expression, params string[] includeList);

		TEntity Insert(TEntity entity);

		int Update(TEntity entity);

		int Delete(TEntity entity);
	}
}
