using FluentValidation;
using Microsoft.AspNetCore.Http;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Business.DependencyResolvers.AutoMapper;
using WebChatApp.DataAccess;
using WebChatApp.Entities.Abstract;
using WebChatApp.Entities.Concrete.Validator;

namespace WebChatApp.Business.Abstract
{
    public class ServiceBase<TEntity>
        where TEntity : EntityBase, new()
    {

        private IRepositoryBase<TEntity> repository;

        protected BaseEntityValidator<TEntity> Validator { get; private set; }
        public BaseEntityValidator<TEntity> UpdateValidator { get; private set; }

        protected readonly IHttpContextAccessor httpContext;

        protected string IpAddress
        {
            get
            {
                return httpContext.HttpContext.Connection.RemoteIpAddress.ToString() + ":" + httpContext.HttpContext.Connection.RemotePort.ToString();
            }
        }

        public AutoMapper.Mapper Mapper { get; private set; }

        public ServiceBase(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            repository=kernel.Get<IRepositoryBase<TEntity>>();

            Validator = kernel.Get<BaseEntityValidator<TEntity>>();


            UpdateValidator = (BaseEntityValidator<TEntity>)Activator.CreateInstance(Validator.GetType());
            UpdateValidator.RuleFor(x => x.UpdateTime).NotEmpty().NotNull();
            UpdateValidator.RuleFor(x => x.UpdateIpAddress).NotEmpty().NotNull();

            Mapper = new AutoMapper.Mapper(new AutoMapper.MapperConfiguration(x => x.AddProfile(new AutoMapperProfile())));

        }

        public void Add(TEntity entity)
        {
            repository.Add(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return repository.Get(filter);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return repository.GetAll(filter);
        }

        //public List<TEntity> GetAll(string sqlQuery)
        //{
        //    return repository.GetAll(sqlQuery);
        //}

        public TEntity GetById(long Id)
        {
            return repository.GetById(Id);
        }

        public void Remove(TEntity entity)
        {
            repository.Delete(entity);
        }

        public void Update(TEntity entity)
        {
            repository.Update(entity);
        }
    }
}
