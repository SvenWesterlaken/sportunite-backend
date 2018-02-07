using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using SportUnite.Domain;
using SportUnite.Logic;
using SportUnite.UI.Extensions;

using SportUnite.UI.Models.ViewModels;
using SportUnite.UI.ViewModels;

namespace SportUnite.UI.Controllers {
    public class BuildingController : Controller {
        private readonly IBuildingManager _buildingManager;

        public BuildingController(IBuildingManager buildingManager) {
            _buildingManager = buildingManager;
        }

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page) {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null) {
                page = 1;
            } else {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var buildings = _buildingManager.Buildings();

            if (!string.IsNullOrEmpty(searchString)) {
                buildings = buildings.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }


            switch (sortOrder) {
                case "name_desc":
                    buildings = buildings.OrderByDescending(b => b.Name).ToList();
                    break;
                default:
                    buildings = buildings.OrderBy(b => b.Name).ToList();
                    break;
            }

            const int pageSize = 10;
            var pageNumber = (page ?? 1);

            return View(buildings.AsQueryable().ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ViewResult AddBuilding() {
            var openingHours = new List<OpeningHours>
            {
                new OpeningHours() {Day = "Maandag"},
                new OpeningHours() {Day = "Dinsdag"},
                new OpeningHours() {Day = "Woensdag"},
                new OpeningHours() {Day = "Donderdag"},
                new OpeningHours() {Day = "Vrijdag"},
                new OpeningHours() {Day = "Zaterdag"},
                new OpeningHours() {Day = "Zondag"}
            };

            var buildingHallVM = new BuildingHallVM {
                OpeningHours = openingHours
            };
            return View(buildingHallVM);
        }

        [HttpPost]
        public ActionResult AddBuilding(BuildingHallVM bhVM) {
           if (ModelState.IsValid) {
                 var building = bhVM.Building;
                building.Halls = new List<Hall>();
                var newHall = new Hall {
                    Name = bhVM.Hall.Name,
                    Size = bhVM.Hall.Size,
                    Price = bhVM.Hall.Price,
                    Available = bhVM.Hall.Available,
                    BuildingId = bhVM.Building.BuildingId
                };

                foreach (var oh in bhVM.OpeningHours) {
                    newHall.HallOpeningHours.Add(
                        new HallOpeningHours {
                            Hall = newHall,
                            OpeningHours = new OpeningHours {
                                Day = oh.Day,
                                OpeningTime = oh.OpeningTime,
                                ClosingTime = oh.ClosingTime
                            }
                        });
                }

                building.Halls.Add(newHall);
                if (_buildingManager.CreateBuilding(building)) {
                    //Correct
                    TempData["notification"] = bhVM.Building.Name.AddMessage("Building");
                    return RedirectToAction("Index");
                } else {
                    //Error
                    TempData["notification"] = bhVM.Building.Name.ErrorAddMessage("Building");
                    return View(bhVM);
                }
           } else {
               TempData["notification"] = bhVM.Building.Name.ErrorAddMessage("Building");
               return View(bhVM);
           }
        }

        [HttpGet]
        public ViewResult UpdateBuilding(int id) {
            return View(_buildingManager.GetBuilding(id));
        }

        [HttpPost]
        public ActionResult UpdateBuilding(Building building) {
            if (ModelState.IsValid) {
                if (_buildingManager.UpdateBuilding(building)) {
                    //Correct
                    TempData["notification"] = building.Name.UpdateMessage("Building");
                    return RedirectToAction("Index");
                } else {
                    //Error
                    TempData["notification"] = building.Name.ErrorUpdateMessage("Building");
                    return View(building);
                }
            } else {
                //Error
                TempData["notification"] = building.Name.ErrorUpdateMessage("Building");
                return View(building);
            }
        }

        public ViewResult GetBuilding(int id) {
            return View("BuildingInformation", _buildingManager.GetBuilding(id));
        }

        public ActionResult DeleteBuilding(Building building) {
            if (_buildingManager.RemoveBuilding(building)) {
                //Correct
                TempData["notification"] = building.Name.RemoveMessage("Building");
                return RedirectToAction("Index");
            } else {
                //Error
                TempData["notification"] = building.Name.ErrorRemoveMessage("Building");
                return RedirectToAction("Index");
            }
        }

		[HttpGet]
		public ActionResult SearchHall(string sortOrder, string currentFilter, string searchString, int? page,
			DateTime startTime, DateTime endTime, int eventId)
		{

			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var buildings = _buildingManager.Buildings();

			if (!string.IsNullOrEmpty(searchString))
			{
				buildings = buildings.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
			}


			switch (sortOrder)
			{
				case "name_desc":
					buildings = buildings.OrderByDescending(s => s.Name);
					break;
				default:
					buildings = buildings.OrderBy(s => s.Name);
					break;
			}

			const int pageSize = 10;
			var pageNumber = (page ?? 1);

			var model = new ReservationHallsModel
			{
				Buildings = buildings.AsQueryable().ToPagedList(pageNumber, pageSize),
				EndTime = endTime,
				StartTime = startTime,
				EventId = eventId
			};

			return View(model);
		}

		[HttpGet]
		public ActionResult SearchUpdateHall(string sortOrder, string currentFilter, string searchString, int? page,
			DateTime startTime, DateTime endTime, int reservationId)
		{

			ViewBag.CurrentSort = sortOrder;
			ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

			if (searchString != null)
			{
				page = 1;
			}
			else
			{
				searchString = currentFilter;
			}

			ViewBag.CurrentFilter = searchString;

			var buildings = _buildingManager.Buildings();

			if (!string.IsNullOrEmpty(searchString))
			{
				buildings = buildings.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
			}


			switch (sortOrder)
			{
				case "name_desc":
					buildings = buildings.OrderByDescending(s => s.Name);
					break;
				default:
					buildings = buildings.OrderBy(s => s.Name);
					break;
			}

			const int pageSize = 10;
			var pageNumber = (page ?? 1);

			var model = new ReservationHallsModel
			{
				Buildings = buildings.AsQueryable().ToPagedList(pageNumber, pageSize),
				EndTime = endTime,
				StartTime = startTime,
				ReservationId = reservationId
			};

			return View(model);
		}
	}
}