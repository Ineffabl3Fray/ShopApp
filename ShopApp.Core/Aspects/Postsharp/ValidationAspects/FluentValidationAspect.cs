using FluentValidation;
using PostSharp.Aspects;
using ShopApp.Core.Validation.FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.Core.Aspects.Postsharp.ValidationAspects
{
    [Serializable]
    public class FluentValidationAspect : OnMethodBoundaryAspect
    {
        private Type _type;

        public FluentValidationAspect(Type type)
        {
            _type = type;
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            var validator = (IValidator)Activator.CreateInstance(_type);
            var entityType = _type.BaseType.GetGenericArguments()[0];
            var entities = args.Arguments.Where(c => c.GetType() == entityType);

            foreach (var item in entities)
            {
                ValidatorTool.FluentValidate(validator, item);
            }
        }
    }
}
