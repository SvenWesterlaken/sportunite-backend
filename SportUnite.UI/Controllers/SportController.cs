using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using SportUnite.Domain;
using SportUnite.Logic;
using SportUnite.UI.Extensions;
using SportUnite.UI.ViewModels;

namespace SportUnite.UI.Controllers
{
    public class SportController : Controller
    {
        private readonly ISportManager _iSportManager;
        private const string Typename = "sport";

        public SportController(ISportManager manager)
        {
            _iSportManager = manager;
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

            var sports = _iSportManager.SportsList();

            if (!string.IsNullOrEmpty(searchString))
            {
                sports = sports.Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            }


            switch (sortOrder)
            {
                case "name_desc":
                    sports = sports.OrderByDescending(s => s.Name).ToList();
                    break;
                default:
                    sports = sports.OrderBy(s => s.Name).ToList();
                    break;
            }

            const int pageSize = 10;
            var pageNumber = (page ?? 1);

            return View(sports.AsQueryable().ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ViewResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddObject(int sportid)
        {
            Sport sport = _iSportManager.Read(sportid);
            if (sport != null)
            {
                SportObjectModel model = new SportObjectModel
                {
                    Sport = sport,
                    SportObject = new SportObject()
                };

                return View(model);
            }
            return View("index");

        }

        [HttpGet]
        public ActionResult Update(int sportid)
        {
            return View(_iSportManager.Read(sportid));
        }

        [HttpGet]
        public ActionResult UpdateObject(int objectid)
        {
            return View(_iSportManager.ReadSportObject(objectid));
        }

        [HttpPost]
        public ActionResult Add(Sport sport)
        {
            if (!ModelState.IsValid) return View("Add");

            try
            {
                _iSportManager.AddSport(sport);
                TempData["notification"] = sport.Name.AddMessage(Typename);
            }
            catch (Exception)
            {
                TempData["notification"] = sport.Name.ErrorAddMessage(Typename);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AddObject(SportObjectModel model, int sportId)
        {
            if (!ModelState.IsValid) return View("AddObject");
            try
            {
                if (model.Sport == null)
                {
                    model.Sport = _iSportManager.Read(sportId);
                }
                _iSportManager.AddSportObject(model.Sport, model.SportObject);
                TempData["notification"] = model.SportObject.Name.AddMessage("Materiaal");
            }
            catch (Exception)
            {
                TempData["notification"] = model.SportObject.Name.ErrorAddMessage("Materiaal");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Update(Sport sport)
        {
            if (!ModelState.IsValid) return View("Update");
            try
            {
                _iSportManager.UpdateSport(sport);
                TempData["notification"] = sport.Name.UpdateMessage(Typename);
            }
            catch (Exception)
            {
                TempData["notification"] = sport.Name.ErrorUpdateMessage(Typename);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UpdateObject(SportObject sportObject)
        {
            if (!ModelState.IsValid) return View("UpdateObject");
            try
            {
                SportObject original = _iSportManager.ReadSportObject(sportObject.SportObjectId);

                if (original != null)
                {
                    original.Name = sportObject.Name;
                    original.SportObjectHalls = sportObject.SportObjectHalls;
                    original.SportObjectSports = sportObject.SportObjectSports;

                    _iSportManager.UpdateSportObject(original);
                    TempData["notification"] = sportObject.Name.UpdateMessage("Materiaal");
                }
            }
            catch (Exception)
            {
                TempData["notification"] = sportObject.Name.ErrorUpdateMessage("Materiaal");
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Delete(int sportid)
        {
            var sport = _iSportManager.Read(sportid);
            try
            {
                
                _iSportManager.DeleteSport(sport);
                TempData["notification"] = sport.Name.RemoveMessage(Typename);
            }
            catch (Exception)
            {
                TempData["notification"] = sport.Name.ErrorRemoveMessage(Typename);
            }
           
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public ActionResult DeleteObject(int objectid)
        {
            var sportObject = _iSportManager.ReadSportObject(objectid);
            try
            {
                _iSportManager.DeleteSportObject(sportObject);
                TempData["notification"] = sportObject.Name.RemoveMessage("Materiaal");
            }
            catch (Exception)
            {
                TempData["notification"] = sportObject.Name.ErrorRemoveMessage("Materiaal");
            }
            
            return RedirectToAction("Index");
        }
    }
}
