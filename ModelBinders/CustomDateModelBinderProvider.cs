using Caso1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Caso1.ModelBinders
{
    public class CustomDateModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(CustomDate))
            {
                return new BinderTypeModelBinder(typeof(CustomDateModelBinder));
            }

            return null;
        }
    }
}
