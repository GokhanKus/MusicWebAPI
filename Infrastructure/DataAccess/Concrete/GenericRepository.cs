using Infrastructure.DataAccess.Abstract;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAccess.Concrete
{
	public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity> where TEntity : class, IEntity, new()
		where TContext : DbContext, new()  //buraya songcontext yazılabiir mi?
	{
		private readonly TContext context;

		//private readonly SongContext context;
		public GenericRepository(TContext tcontext)
		{
			context = tcontext;
		}
		public int Delete(TEntity entity)
		{
			var deletedEntity = context.Entry(entity);
			deletedEntity.State = EntityState.Deleted;
			return context.SaveChanges();
		}

		public TEntity Get(Expression<Func<TEntity, bool>> expression, params string[] includeList)
		{
			//x=>x.Id==ıd
			//_context.artist.Where(i=>i.Id = artistId).First()

			IQueryable<TEntity> dbSet = context.Set<TEntity>();

			if (includeList != null)
			{
				foreach (var item in includeList)
				{
					dbSet = dbSet.Include(item);
				}
			}
			var entity = dbSet.FirstOrDefault(expression);
			return entity;
		}
		public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, params string[] includeList)
		{
			IQueryable<TEntity> dbSet = context.Set<TEntity>();

			//context.Set<Artist>()
			//var dbset = context.Artist

			if (expression != null)
			{
				dbSet = dbSet.Where(expression);
			}
			if (includeList != null)
			{
				foreach (var item in includeList)
				{
					dbSet = dbSet.Include(item);
				}
			}
			return dbSet.ToList();
			//context.Artist.ToList();
		}
		public TEntity Insert(TEntity entity)
		{
			var addedEntity = context.Entry(entity);
			addedEntity.State = EntityState.Added;
			context.SaveChanges();
			return addedEntity.Entity;
		}
		public int Update(TEntity entity)
		{
			var updatedEntity = context.Entry(entity);
			updatedEntity.State = EntityState.Modified;
			return context.SaveChanges();
		}

	}
}
