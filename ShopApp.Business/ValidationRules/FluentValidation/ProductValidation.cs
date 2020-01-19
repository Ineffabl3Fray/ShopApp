using FluentValidation;
using ShopApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Validation.FluentValidation
{
    public class ProductValidation: AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Name).NotEmpty().Length(2, 60);
            RuleFor(c => c.Description).NotEmpty().Length(10, 250);
            RuleFor(c => c.ImageUrl).NotEmpty().NotNull();
            RuleFor(c => c.Price).NotNull().NotEmpty().GreaterThanOrEqualTo(0);
        }
    }
}
