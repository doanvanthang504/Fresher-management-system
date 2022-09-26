using Global.Shared.Commons;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace WebAPI.ModelBinders
{
    public class DateOnlyModelBinder : IModelBinder
    {
        public bool IsNullableSupported { get; init; }

        public DateOnlyModelBinder(bool isNullableSupported)
        {
            IsNullableSupported = isNullableSupported;
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var modelName = bindingContext.ModelName;

            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var rawValue = valueProviderResult.FirstValue;

            if (string.IsNullOrWhiteSpace(rawValue))
            {
                if (IsNullableSupported)
                {
                    bindingContext.Result = ModelBindingResult.Success(null);
                    return Task.CompletedTask;
                }
                else
                {
                    bindingContext.ModelState.TryAddModelError(modelName, Constant.INVALID_DATE_STRING);
                    return Task.CompletedTask;
                }
            }

            var isValidDateOnly = DateOnly.TryParseExact(rawValue, Constant.DATE_TIME_FORMAT_MMddyyyy, out DateOnly value);

            if (!isValidDateOnly)
            {
                bindingContext.ModelState.TryAddModelError(modelName, Constant.INVALID_DATE_STRING);
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(value);
            return Task.CompletedTask;
        }
    }
}
