using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;
using SportUnite.Domain;
using SportUnite.Logic;
using System.Linq;
using SportUnite.UI.Extensions;
using SportUnite.UI.ViewModels;

namespace SportUnite.UI.Controllers {
    public class HallController : Controller {
        private readonly IHallManager _hallManager;
        private readonly IBuildingManager _buildingManager;
        private readonly ISportManager _sportManager;

        public HallController(IHallManager hallManager, IBuildingManager buildingManager, ISportManager sportManager) {
            _hallManager = hallManager;
            _buildingManager = buildingManager;
            _sportManager = sportManager;
        }

		public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
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

			var halls = _hallManager.Halls();

			if (!string.IsNullOrEmpty(searchString))
			{
				halls = halls.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
			}


			switch (sortOrder)
			{
				case "name_desc":
					halls = halls.OrderByDescending(b => b.Name).ToList();
					break;
				default:
					halls = halls.OrderBy(b => b.Name).ToList();
					break;
			}

			const int pageSize = 10;
			var pageNumber = (page ?? 1);

			return View(halls.AsQueryable().ToPagedList(pageNumber, pageSize));
		}

        [HttpGet]
        public ViewResult AddHall() {
            var buildings = _buildingManager.Buildings();
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

            var hallOpeningHoursVM = new HallOpeningHoursVM {
                Buildings = buildings.ToList(),
                OpeningHours = openingHours
            };
            return View(hallOpeningHoursVM);
        }

        [HttpPost]
        public ActionResult AddHall(HallOpeningHoursVM hohVM) {
            if (ModelState.IsValid) {
                var newHall = new Hall {
                    Name = hohVM.Hall.Name,
                    Size = hohVM.Hall.Size,
                    Price = hohVM.Hall.Price,
                    Available = hohVM.Hall.Available,
                    BuildingId = hohVM.BuildingId
                };

                foreach (var oh in hohVM.OpeningHours) {
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
                if (_hallManager.CreateHall(newHall)) {
                    //Correct
                    TempData["notification"] = hohVM.Hall.Name.AddMessage("Hall");
                    return RedirectToAction("Index");
                } else {
                    //Error
                    TempData["notification"] = hohVM.Hall.Name.ErrorAddMessage("Hall");
                    return View(hohVM);
                }
            } else {
                TempData["notification"] = hohVM.Hall.Name.ErrorAddMessage("Hall");
                return View(hohVM);
            }
        }

        [HttpGet]
        public ViewResult UpdateHall(int id) {
            var hall = _hallManager.GetHall(id);
            var hallOpeningHoursVM = new HallOpeningHoursVM {
                Hall = new Hall {
                    HallId = hall.HallId,
                    Name = hall.Name,
                    Size = hall.Size,
                    Price = hall.Price,
                    Available = hall.Available
                },
                OpeningHours = new List<OpeningHours>(),
                SportManager = _sportManager,
                SportObjects = _sportManager.GetSportObjectsFromHall(hall)
            };

            foreach (var oh in hall.HallOpeningHours) {
                hallOpeningHoursVM.OpeningHours.Add(new OpeningHours() {
                    OpeningHoursId = oh.OpeningHours.OpeningHoursId,
                    Day = oh.OpeningHours.Day,
                    OpeningTime = oh.OpeningHours.OpeningTime,
                    ClosingTime = oh.OpeningHours.ClosingTime
                });

            }
            return View("UpdateHall", hallOpeningHoursVM);
        }

        [HttpPost]
        public ActionResult UpdateHall(HallOpeningHoursVM hohVM, string[] openingTimes, string[] closingTimes/*, int[] sportObjects*/)
        {
            var i = 0;
            foreach (var openingHour in openingTimes)
            {
                hohVM.OpeningHours[i].OpeningTime = openingHour;
                i++;
            }

            var j = 0;
            foreach (var closingHour in closingTimes)
            {
                hohVM.OpeningHours[j].ClosingTime = closingHour;
                j++;
            }

            for (var k = 0; k < 7; k++)
            {
                ModelState.Remove("OpeningHours[" + k + "].OpeningTime");
                ModelState.Remove("OpeningHours[" + k + "].ClosingTime");
            }

            if (ModelState.IsValid) {
                var hallToUpdate = _hallManager.GetHall(hohVM.Hall.HallId);
                hallToUpdate.Name = hohVM.Hall.Name;
                hallToUpdate.Size = hohVM.Hall.Size;
                hallToUpdate.Price = hohVM.Hall.Price;
                hallToUpdate.Available = hohVM.Hall.Available;
                hallToUpdate.HallOpeningHours = new List<HallOpeningHours>();

				//_sportManager.UpdateHallObjects(hallToUpdate, sportObjects);

                foreach (var oh in hohVM.OpeningHours) {
                    hallToUpdate.HallOpeningHours.Add(
                        new HallOpeningHours { HallId = hallToUpdate.HallId, OpeningHoursId = oh.OpeningHoursId, OpeningHours = new OpeningHours { OpeningHoursId = oh.OpeningHoursId, Day = oh.Day, OpeningTime = oh.OpeningTime, ClosingTime = oh.ClosingTime } });

                }
                if (_hallManager.UpdateHall(hallToUpdate)) {
                    //Correct
                    TempData["notification"] = hohVM.Hall.Name.UpdateMessage("Hall");
                    return RedirectToAction("Index");
                } else {
                    //Error
                    TempData["notification"] = hohVM.Hall.Name.ErrorUpdateMessage("Hall");
                    return View(hohVM);
                }
            } else {
                TempData["notification"] = hohVM.Hall.Name.ErrorUpdateMessage("Hall");
                hohVM.SportManager = _sportManager;
                return View(hohVM);
            }
        }

		public ViewResult GetHall(int id)
		{
			return View("HallInformation", _hallManager.GetHall(id));
		}

        public ActionResult DeleteHall(Hall hall) {
            if (_hallManager.RemoveHall(hall)) {
                //Successful
                TempData["notification"] = hall.Name.RemoveMessage("Hall");
                return RedirectToAction("Index");
            } else {
                //Error
                TempData["notification"] = hall.Name.ErrorRemoveMessage("Hall");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ViewResult UpdateOpeningHours(Hall hall) {
            if (ModelState.IsValid) {
                if (_hallManager.UpdateOpeningHours(hall) != null) {
                    //Correct
                    return View("Index");
                } else {
                    //Error
                    return View("Index");
                }
            } else {
                //Error
                return View("Index");
            }
        }

        [HttpPost]
        public ViewResult AddOpeningHours(HallOpeningHoursVM hohVM) {
            if (ModelState.IsValid) {
                var hohToAdd = new HallOpeningHours() {
                    HallId = hohVM.Hall.HallId,
                    OpeningHours = new OpeningHours() {
                        Day = hohVM.Day,
                        OpeningTime = hohVM.OpeningTime,
                        ClosingTime = hohVM.ClosingTime
                    }
                };
                var hall = new Hall() {
                    HallId = hohVM.Hall.HallId,
                    HallOpeningHours = new List<HallOpeningHours>() { hohToAdd }
                };

                if (_hallManager.CreateOpeningHours(hall)) {
                    //Correct
                    return View("Index");
                } else {
                    //Error
                    return View("Index");
                }
            } else {
                //Error
                return View("Index");
            }
        }

        [HttpPost]
        public ViewResult DeleteOpeningHours(HallOpeningHoursVM hohVM) {
            if (ModelState.IsValid) {
                var hohToRemove = new HallOpeningHours() {
                    HallId = hohVM.Hall.HallId,
                    OpeningHoursId = hohVM.OpeningHours.FirstOrDefault().OpeningHoursId
                };
                if (_hallManager.RemoveHallOpeningHours(hohToRemove)) {
                    //Correct
                    return View("Index");
                } else {
                    //Error
                    return View("Index");
                }
            } else {
                //Error
                return View("Index");
            }
        }
    }
}