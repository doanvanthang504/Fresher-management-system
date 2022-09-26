using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace WebAPI.ModelBinders.Providers
{
    public class DateOnlyMoldelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(DateOnly))
            {
                return new DateOnlyModelBinder(false);
            }
            else if (context.Metadata.ModelType == typeof(DateOnly?))
            {
                return new DateOnlyModelBinder(true);
            }

            return null;
        }
    }
}
