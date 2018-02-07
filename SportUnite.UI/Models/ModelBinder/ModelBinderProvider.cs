using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using SportUnite.Domain;
using SportUnite.UI.Models.ModelBinder;
using SportUnite.UI.Models.ViewModels;

namespace SportUnite.UI.Models.ModelBinder
{
    public class ModelBinderProvider : IModelBinderProvider
    {

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(ReservationModel))
            {
                return new BinderTypeModelBinder(typeof(ReservationModelBinder));
            }

            return null;
        }
    }
}
