using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportUnite.Domain;
using SportUnite.UI.Models.ViewModels;

namespace SportUnite.UI.Models.ModelBinder
{
    public class ReservationModelBinder : IModelBinder
    { 
        public Task BindModelAsync(ModelBindingContext modelBindingContext)
        {



            if (!int.TryParse(modelBindingContext.ActionContext.HttpContext.Request.Form["Reservation.ReservationId"], out var reservationId)) {
                reservationId = 0;
            }

	        if (!int.TryParse(modelBindingContext.ActionContext.HttpContext.Request.Form["Reservation.HallId"], out var hallId))
	        {
		        hallId = 0;
	        }

					if (!bool.TryParse(
			        modelBindingContext.ActionContext.HttpContext.Request.Form["Reservation.Definite"],
			        out var definite))
		        {
			        definite = false;
		        }

			if (!DateTime.TryParse(
                    modelBindingContext.ActionContext.HttpContext.Request.Form["Reservation.StartTime.Date"],
                    out var dateTime) ||
							!TimeSpan.TryParse(
                    modelBindingContext.ActionContext.HttpContext.Request.Form["Reservation.TimeFinish.Time"],
                    out var timeFinishTime) ||
                !TimeSpan.TryParse(
                    modelBindingContext.ActionContext.HttpContext.Request.Form["Reservation.StartTime.Time"],
                    out var timeStartTime) ||
                !int.TryParse(modelBindingContext.ActionContext.HttpContext.Request.Form["Reservation.SportEventId"],
                    out var sportEventId))
               

            {
                var modelfail = new ReservationModel()
                {
                    Reservation = new Reservation()
                    {

                        TimeFinish = dateTime.Add(timeFinishTime),
                        StartTime = dateTime.Add(timeStartTime),
												HallId = hallId,
												ReservationId = reservationId,
                        Definite = definite
                    }
                    
                };
                modelBindingContext.Result = ModelBindingResult.Success(modelfail);

               
                return Task.CompletedTask;
            }

          
            var model = new ReservationModel
            {
                Reservation = new Reservation
                {
                    TimeFinish = dateTime.Add(timeFinishTime),
                    StartTime = dateTime.Add(timeStartTime),
                    SportEventId = sportEventId,
                    ReservationId =  reservationId,
										HallId = hallId,
                                        Definite = definite
 
                }
            };

            modelBindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }

      
    }
}
