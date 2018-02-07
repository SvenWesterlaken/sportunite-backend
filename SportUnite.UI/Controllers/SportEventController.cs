using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SportUnite.Data;
using SportUnite.Domain;
using SportUnite.Logic;
using SportUnite.UI.Extensions;
using SportUnite.UI.Extentions;
using SportUnite.UI.Models.ViewModels;
using SportUnite.UI.ViewModels;

namespace SportUnite.UI.Controllers
{
	[Authorize]
	public class SportEventController : Controller
	{
		private readonly ISportEventManager _sportEventManager;
		private readonly ISportManager _sportManager;
		private const string Sportevent = "Sport event";

		public SportEventController(ISportEventManager sportEventManager, ISportManager sportManager)
		{
			_sportEventManager = sportEventManager;
			_sportManager = sportManager;
		}

		public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
		{
			ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
			ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";


			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var sportEvents = _sportEventManager.ViewAllSportEvents();

			if (!string.IsNullOrEmpty(searchString))
			{
				sportEvents = sportEvents.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
			}

			switch (sortOrder)
			{
				case "date_desc":
					sportEvents = sportEvents.OrderByDescending(se => se.EventStartTime).ToList();
					break;
				case "Name":
					sportEvents = sportEvents.OrderBy(se => se.Name).ToList();
					break;
				case "name_desc":
					sportEvents = sportEvents.OrderByDescending(se => se.Name).ToList();
					break;
				case "Date":
					sportEvents = sportEvents.OrderBy(se => se.EventStartTime).ToList();
					break;
				default:
					sportEvents = sportEvents.OrderByDescending(se => se.EventStartTime).ToList();
					break;
			}

			ICollection<SportEventViewModel> viewModels = new List<SportEventViewModel>();
			foreach (var se in sportEvents)
			{
				viewModels.Add(new SportEventViewModel
				{
					MaxAttendees = se.MaxAttendees,
					MinAttendees = se.MinAttendees,
					Name = se.Name,
					SportEventDate = se.EventStartTime.FormatDate(),
					SportEventId = se.SportEventId,
					SportEventStartAndEndTime = $"{se.EventStartTime.FormatTime()} - {se.EventEndTime.FormatTime()}",
					HasReservation = se.Reservation != null,
					Sport = se.Sport
				});
			}

			const int pageSize = 10;
			var pageNumber = (page ?? 1);

			return View(viewModels.AsQueryable().ToPagedList(pageNumber, pageSize));
		}

		[HttpGet]
		public ViewResult Add()
		{
			var sports = _sportManager.SportsList();
			var addSportEventViewModel = new AddSportEventViewModel
			{

				Sports = sports
			};
			return View(addSportEventViewModel);
		}

		[HttpPost]
		public ActionResult Add(AddSportEventViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_sportEventManager.AddSportEvent(viewModel.SportEvent);
					TempData["notification"] = viewModel.SportEvent.Name.AddMessage("Sportevent");
					return RedirectToAction("Index");
				}
				catch (DbUpdateException db)
				{
					TempData["notification"] = viewModel.SportEvent.Name.ErrorAddMessage("Sportevent");
				}
				catch (Exception e)
				{
					TempData["notification"] = viewModel.SportEvent.Name.ErrorAddMessage("Sportevent");
				}

			}
			else
			{
				TempData["notification"] = viewModel.SportEvent.Name.ErrorAddMessage("Sportevent");
			}

			var sports = _sportManager.SportsList();
			viewModel.Sports = sports;

			return View(viewModel);
		}

		[HttpGet]
		public ActionResult Update(int sportEventId)
		{
			var sports = _sportManager.SportsList();
			SportEvent sportEventToUpdate = _sportEventManager.ViewSportEvent(sportEventId);
			var addSportEventViewModel = new AddSportEventViewModel
			{
				SportEvent = sportEventToUpdate,
				Sports = sports
			};

			return View(addSportEventViewModel);
		}

		[HttpPost]
		public ActionResult Update(AddSportEventViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_sportEventManager.EditSportEvent(viewModel.SportEvent);
					TempData["notification"] = viewModel.SportEvent.Name.UpdateMessage("Sportevent");
					return RedirectToAction("Index");
				}
				catch (DbUpdateException db)
				{
					TempData["notification"] = viewModel.SportEvent.Name.InUseRemoveMessage("Sportevent");
				}
				catch (Exception e)
				{
					TempData["notification"] = viewModel.SportEvent.Name.ErrorUpdateMessage("Sportevent");
				}

			}
			else
			{
				TempData["notification"] = viewModel.SportEvent.Name.ErrorUpdateMessage("Sportevent");
			}
			var sports = _sportManager.SportsList();
			viewModel.Sports = sports;

			return View("Update", viewModel);

		}

		[HttpGet]
		public ActionResult Read(int sportEventId)
		{
			SportEvent sportEventToRead = _sportEventManager.ViewSportEvent(sportEventId);


			

			ReadSportEventViewModel viewModel;
			if (sportEventToRead.Reservation != null)
			{
				var buildingStreetName = sportEventToRead.Reservation.Hall.Building.Address.StreetName;
				var buildingHouseNumber = sportEventToRead.Reservation.Hall.Building.Address.HouseNumber;
				var buildingSuffix = sportEventToRead.Reservation.Hall.Building.Address.Suffix;
				var buildingCity = sportEventToRead.Reservation.Hall.Building.Address.City;

				viewModel = new ReadSportEventViewModel
				{
					SportEventId = sportEventToRead.SportEventId,
					SportEventName = sportEventToRead.Name,
					SportName = sportEventToRead.Sport.Name,
					SportEventEventTime =
						$"{sportEventToRead.EventStartTime.FormatTime()} - {sportEventToRead.EventEndTime.FormatTime()}",
					SportEventDate = sportEventToRead.EventStartTime.FormatDate(),
					MinAttendees = sportEventToRead.MinAttendees,
					MaxAttendees = sportEventToRead.MaxAttendees,
					SportEventDescription = sportEventToRead.Description,
					ReservationId = sportEventToRead.Reservation.ReservationId,
					Definite = sportEventToRead.Reservation.Definite ? "ja" : "nee",
					HallName = sportEventToRead.Reservation.Hall.Name,
					BuildingName = sportEventToRead.Reservation.Hall.Building.Name,
					BuildingAddress = $"{buildingStreetName} {buildingHouseNumber} {buildingSuffix}, {buildingCity}"

				};
			}
			else
			{
				viewModel = new ReadSportEventViewModel
				{
					SportEventId = sportEventToRead.SportEventId,
					SportEventName = sportEventToRead.Name,
					SportName = sportEventToRead.Sport.Name,
					SportEventEventTime =
						$"{sportEventToRead.EventStartTime.FormatTime()} - {sportEventToRead.EventEndTime.FormatTime()}",
					SportEventDate = sportEventToRead.EventStartTime.FormatDate(),
					MinAttendees = sportEventToRead.MinAttendees,
					MaxAttendees = sportEventToRead.MaxAttendees,
					SportEventDescription = sportEventToRead.Description,
				};
			}
			
			return View(viewModel);
		}


		[HttpPost]
		public ActionResult Delete(SportEvent sportEvent)
		{
			
			try
			{
				_sportEventManager.DeleteSportEvent(sportEvent.SportEventId);
				TempData["notification"] = sportEvent.Name.RemoveMessage("Sportevent");
			}
			catch (DbUpdateException db)
			{
				TempData["notification"] = sportEvent.Name.InUseRemoveMessage("Sportevent");
			}
			catch (Exception e)
			{
				TempData["notification"] = sportEvent.Name.ErrorRemoveMessage("Sportevent");
			}
				
			
			return RedirectToAction("Index");
		}
	}
}