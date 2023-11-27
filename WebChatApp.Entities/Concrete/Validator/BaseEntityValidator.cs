using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebChatApp.Entities.Abstract;

namespace WebChatApp.Entities.Concrete.Validator
{
    public class BaseEntityValidator<TEntity>:AbstractValidator<TEntity>
        where TEntity : EntityBase, new()
    {
        public BaseEntityValidator()
        {
            RuleFor(x => x.CreateIpAddress).NotNull().NotEmpty().WithMessage("Zorunlu");
            RuleFor(x => x.CreateTime).NotNull().NotEmpty().WithMessage("Zorunlu");

        }
    }
}
