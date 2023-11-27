using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Entities.Abstract;

namespace WebChatApp.DataAccess
{
    public class GenericRepositoryBase<TContext, TEntity> : IRepositoryBase<TEntity>
        where TContext : DbContext,new()
        where TEntity : EntityBase, new()
    {
        public TEntity Add(TEntity entity)
        {
            using(var context= new TContext())
            {
                var entry=context.Entry(entity);
                entry.State = EntityState.Added;
                
                context.SaveChanges();
            }
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Deleted;

                context.SaveChanges();
            }
            return entity;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

       
        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter=null)
        {
            using (var context = new TContext())
            {

                return (filter!=null)
                    ?context.Set<TEntity>().Where(filter).ToList()
                    : context.Set<TEntity>().ToList();
            }
        }

        public TEntity GetById(long id)
        {
            using (var context = new TContext())
            {
               return context.Set<TEntity>().FirstOrDefault(x=>x.Id==id);
            }
           
        }

        public TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var entry = context.Entry(entity);
                entry.State = EntityState.Modified;

                context.SaveChanges();
            }
            return entity;
        }
    }
}
