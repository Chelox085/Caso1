using System.Globalization;
using Caso1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Caso1.ModelBinders
{
    public class CustomDateModelBinder : IModelBinder
    {
        private static readonly string[] Formats =
        {
            "dd/MM/yyyy",
            "dd-MM-yyyy",
            "yyyy-MM-dd"
        };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var rawValue = valueProviderResult.FirstValue ?? string.Empty;
            if (string.IsNullOrWhiteSpace(rawValue))
            {
                return Task.CompletedTask;
            }

            if (DateTime.TryParseExact(rawValue, Formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                bindingContext.Result = ModelBindingResult.Success(new CustomDate
                {
                    Raw = rawValue,
                    Value = date
                });
            }
            else
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Formato de fecha inválido. Use dd/MM/yyyy o yyyy-MM-dd.");
            }

            return Task.CompletedTask;
        }
    }
}
