using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Entities.Concrete;

namespace WebChatApp.DataAccess.EntityFramework
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext():base("server=.;Initial Catalog= ChatDB;Integrated Security=True")
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();


            base.OnModelCreating(modelBuilder);
        }



        public DbSet<ChatEntity> Chats { get; set; }

        public DbSet<MessageEntity> Messages { get; set; }
    }
}
