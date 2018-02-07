using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportUnite.Domain;
using SportUnite.UI.Models.ViewModels;
using SportUnite.UI.ViewModels;

namespace SportUnite.UI.Models.ModelBinder
{
	public class SportEventModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext modelBindingContext)
		{


			
			if (!DateTime.TryParse(
					modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.EventStartTime.Date"],
					out var dateTime) ||
				!TimeSpan.TryParse(
					modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.EventEndTime.Time"],
					out var timeFinishTime) ||
				!TimeSpan.TryParse(
					modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.EventStartTime.Time"],
					out var timeStartTime) ||
				!int.TryParse(modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.SportId"],
					out var sportId) ||
			    !int.TryParse(modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.MinAttendees"],
				    out  var minAttendees) ||
			    !int.TryParse(modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.MaxAttendees"],
				    out var maxAttendees) ||
			    !int.TryParse(modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.SportEventId"],
				    out var sportEventId)
			)
			{
				var modelfail = new AddSportEventViewModel()
				{
					SportEvent = new SportEvent
					{
						Description = modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.Description"],
						EventEndTime = dateTime.Add(timeFinishTime),
						EventStartTime = dateTime.Add(timeStartTime),
						Name = modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.Name"],
						MinAttendees = 0,
						MaxAttendees = 0,
					},
					AddReservation = true
				};

				modelBindingContext.Result = ModelBindingResult.Success(modelfail);


				return Task.CompletedTask;
			}

			var model = new AddSportEventViewModel()
			{
				SportEvent = new SportEvent
				{
					Description = modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.Description"],
					EventEndTime = dateTime.Add(timeFinishTime),
					EventStartTime = dateTime.Add(timeStartTime),
					Name = modelBindingContext.ActionContext.HttpContext.Request.Form["SportEvent.Name"],
					MinAttendees = minAttendees,
					MaxAttendees = maxAttendees,
					SportId = sportId,
					SportEventId = sportEventId
				},
				AddReservation = false
			};

			modelBindingContext.Result = ModelBindingResult.Success(model);
			return Task.CompletedTask;
		}
	}
}
