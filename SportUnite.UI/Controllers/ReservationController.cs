using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using SportUnite.Domain;
using SportUnite.Logic;
using SportUnite.UI.Extensions;
using SportUnite.UI.Models.ViewModels;



namespace SportUnite.UI.Controllers
{
	[Authorize]
	public class ReservationController : Controller
	{
		private readonly ISportEventManager _sportEventManager;
		private readonly IHallManager _hallManager;
		private readonly IBuildingManager _buildingManager;
		private const string Typename = "reservatie";


		public ReservationController(ISportEventManager sportEventManager, IHallManager hallManager, IBuildingManager buildingManager)
		{
			_hallManager = hallManager;
			_sportEventManager = sportEventManager;
			_buildingManager = buildingManager;
		}

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "reservationId_desc" : "";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;


			var reservations = _sportEventManager.Reservations();

            if (!string.IsNullOrEmpty(searchString))
            {
                reservations = reservations.Where(s => s.SportEvent.Name.ToLower().Contains(searchString.ToLower()));
            
            }


            switch (sortOrder)
            {
               
                default:
                    reservations = reservations.OrderBy(s => s.StartTime).ToList();
                    break;
            }

			const int pageSize = 10;
			var pageNumber = (page ?? 1);

			var model = new ReservationModel
			{
				Reservations = reservations
			};


			return View(model.Reservations.AsQueryable().ToPagedList(pageNumber, pageSize));
		}

		[HttpGet]
		public IActionResult ChooseUpdateReservation(int reservationId)
		{
			var reservation = _sportEventManager.GetReservation(reservationId);

			var model = new ReservationUpdateModel
			{
				ReservationId = reservationId,
				EndTime = reservation.TimeFinish,
				StartTime = reservation.StartTime
			};
			return View(model);
		}

		[HttpGet]
		public IActionResult UpdateReservation(int reservationId) {
			var reservation = _sportEventManager.GetReservation(reservationId);
			var hall = _hallManager.GetHallReservation(reservation.HallId);
			hall.Building = _buildingManager.GetBuilding(hall.BuildingId);
			reservation.Hall = hall;

			var model = new ReservationModel
			{
				Reservation = reservation,
				SportEvents = _sportEventManager.ViewAllSportEvents()
			};


			return View(model);

		}

		[HttpPost]
		public IActionResult UpdateReservationHall(ReservationHallsModel model)
		{
			var hall = _hallManager.GetHallReservation(model.HallId);
			hall.Building = _buildingManager.GetBuilding(hall.BuildingId);

			var reservation = _sportEventManager.GetReservation(model.ReservationId);
			reservation.StartTime = model.StartTime;
			reservation.TimeFinish = model.EndTime;
			reservation.HallId = model.HallId;
			reservation.Hall = hall;


			var filledreservation = new ReservationModel
			{
				Reservation = reservation,
				SportEvents = _sportEventManager.ViewAllSportEvents()
			};

			return View("UpdateReservation", filledreservation);
		}

		[HttpPost]
		public IActionResult UpdateReservation(ReservationModel reservationModel)
		{
			var hall = _hallManager.GetHallReservation(reservationModel.Reservation.HallId);
			hall.Building = _buildingManager.GetBuilding(hall.BuildingId);
            reservationModel.Reservation.SportEvent = _sportEventManager.ViewSportEvent(reservationModel.Reservation.SportEventId);
            
            
            if (ModelState.IsValid && _sportEventManager.UpdateReservation(reservationModel.Reservation))
			{

				TempData["notification"] = reservationModel.Reservation.ReservationId.ToString().UpdateMessage(Typename);

				return RedirectToAction("Index");
			}

		    reservationModel.Reservation.Hall = hall;


            return View(reservationModel);

		}

		[HttpGet]
		public IActionResult AddReservation()
		{

			var model = new ReservationModel
			{

				SportEvents = _sportEventManager.GetAllEventsWithoutReservation()

			};


			return View(model);
		}

		[HttpPost]
		public ActionResult AddReservation(ReservationModel reservationModel)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("SearchHall", "Building", new
				{
					startTime = reservationModel.Reservation.StartTime,
					endTime = reservationModel.Reservation.TimeFinish,
					eventId = reservationModel.Reservation.SportEventId
				});
			}

			ModelState.AddModelError("AddError", "Er is iets misgegaan met het toevoegen van de reservatie. Probeer het opnieuw");
			reservationModel.SportEvents = _sportEventManager.GetAllEventsWithoutReservation();

			if (DateTime.MinValue == reservationModel.Reservation.StartTime) {
				reservationModel.Reservation.StartTime = DateTime.Now;
			}

			if (DateTime.MinValue == reservationModel.Reservation.TimeFinish) {
				reservationModel.Reservation.TimeFinish = DateTime.Now.AddHours(1);
			}

			return View(reservationModel);
		}



		[HttpPost]
		public IActionResult DeleteReservation(int reservationid)
		{
			var reservation = _sportEventManager.GetReservation(reservationid);


			try
			{
				if (_sportEventManager.DeleteReservation(reservation))
				{
					TempData["Notification"] = reservation.ReservationId.ToString().RemoveMessage(Typename);
					return RedirectToAction("Index");
				}
			}
			catch (Exception)
			{
				TempData["Notification"] = reservation.ReservationId.ToString().InUseRemoveMessage(Typename);
				return RedirectToAction("Index");
			}

			TempData["Notifcation"] = reservation.ReservationId.ToString().ErrorRemoveMessage(Typename);
			return RedirectToAction("Index");

		}

		[HttpPost]
		public ActionResult Confirmation(ReservationHallsModel model)
		{
			var hall = _hallManager.GetHallReservation(model.HallId);
			hall.Building = _buildingManager.GetBuilding(hall.BuildingId);
			var sportevent = _sportEventManager.ViewSportEvent(model.EventId);

			var reservation = new Reservation
			{
				StartTime = model.StartTime,
				TimeFinish = model.EndTime,
				SportEventId = model.EventId,
				SportEvent = sportevent,
				Hall = hall,
				HallId = model.HallId
			};

			return View(reservation);
		}

		[HttpPost]
		public ActionResult SaveReservation(Reservation reservation)
		{
			if (!ModelState.IsValid || !_sportEventManager.AddReservation(reservation)) return View("Confirmation", reservation);

		    var name = _sportEventManager.ViewSportEvent(reservation.SportEventId).Name;

			TempData["notification"] = name.AddMessage(Typename);
			return RedirectToAction("Index");
		}
	}

}

