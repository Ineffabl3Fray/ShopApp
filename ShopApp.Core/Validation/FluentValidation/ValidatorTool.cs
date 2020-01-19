using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Core.Validation.FluentValidation
{
    public class ValidatorTool
    {
        public static string errorMessage { get; set; }
        public static void FluentValidate(IValidator validator, object entity)
        {
            errorMessage = string.Empty;
            var result = validator.Validate(entity);
            if (result.Errors.Count > 0)
            {
                foreach (var item in result.Errors)
                {
                    errorMessage += item.ErrorMessage;
                }
            }
        }
    }
}
