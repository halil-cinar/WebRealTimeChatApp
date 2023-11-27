using Ninject.Modules;
using WebChatApp.DataAccess;
using WebChatApp.DataAccess.EntityFramework;
using WebChatApp.Entities.Concrete;

namespace WebChatApp.Business.DependencyResolvers.Ninject
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            //Bind<BaseEntityValidator<ChatEntity>>().To<ChatValidator>();
           
            //Bind<BaseEntityValidator<MessageEntity>>().To<MessageValidator>();
            


            Bind<IRepositoryBase<ChatEntity>>().To<EfEntityRepositoryBase<ChatEntity>>();
            
            Bind<IRepositoryBase<MessageEntity>>().To<EfEntityRepositoryBase<MessageEntity>>();
           



        }
    }
}
