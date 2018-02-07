using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using SportUnite.UI.Models.ViewModels;
using SportUnite.UI.ViewModels;

namespace SportUnite.UI.Models.ModelBinder
{
    public class SportEventModelBinderProvider : IModelBinderProvider
    {
	    public IModelBinder GetBinder(ModelBinderProviderContext context)
	    {
		    if (context == null)
		    {
			    throw new ArgumentNullException(nameof(context));
		    }

		    if (context.Metadata.ModelType == typeof(AddSportEventViewModel))
		    {
			    return new BinderTypeModelBinder(typeof(SportEventModelBinder));
		    }

		    return null;
	    }

    }
}
