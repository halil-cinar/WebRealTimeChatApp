using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Entities.Abstract;

namespace WebChatApp.DataAccess
{
    public interface IRepositoryBase<T>
        where T : EntityBase,new()
    {
        public T Get(Expression<Func<T, bool>> filter);
        public List<T> GetAll(Expression<Func<T, bool>> filter);

        public T GetById(long id);
        public T Update(T entity);
        public T Delete(T entity);   
        public T Add(T entity);

    }
}
