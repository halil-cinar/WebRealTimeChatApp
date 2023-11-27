using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Entities.Abstract;

namespace WebChatApp.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity>:GenericRepositoryBase<DatabaseContext,TEntity>,IRepositoryBase<TEntity>
        where TEntity : EntityBase, new()
    {

    }
}
