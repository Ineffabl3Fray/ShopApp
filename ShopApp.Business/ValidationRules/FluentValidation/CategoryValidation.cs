using FluentValidation;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Validation.FluentValidation
{
    public class CategoryValidation: AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(c => c.Name).NotEmpty().Length(2, 80);
        }
    }
}
