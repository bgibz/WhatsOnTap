using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanBrewList.Models;
using VanBrewList.Services;
using VanBrewList.Authorize;
using MongoDB.Driver;
using MongoDB.Bson;

namespace VanBrewList.Controllers
{
    public class BreweryController : Controller
    {
        private MongoService mongoService;

        public BreweryController()
        {
            this.mongoService = new MongoService();
        }

        // GET: Brewery
        public ActionResult Index()
        {
            ICollection<Brewery> brewList = mongoService.GetBreweries();

            return View("Index", brewList);
        }

        // GET: Brewery/Details/5
        public ActionResult Details(string id)
        {
            var brewery = mongoService.GetBrewery(id);
            foreach(Beer b in brewery.Growlers)
            {
                b.setImg();
            }
            foreach(Beer b in brewery.TastingRoom)
            {
                b.setImg();
            }

            return View("Detail", brewery);
        }

        // GET: Brewery/Create
        [BrewAuthorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brewery/Create
        [HttpPost]
        [BrewAuthorize]
        public ActionResult Create(Brewery brewery)
        {
            if (!mongoService.checkBrewName(brewery.Name))
            {
                TempData["Error"] = "Brewery already exists!";
                return View();
            }
            try
            {
                brewery.Growlers = new List<Beer>();
                brewery.TastingRoom = new List<Beer>();
                mongoService.createBrewery(brewery);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brewery/Edit/id
        [Authorize]
        public ActionResult Edit(string id)
        {
            var brewery = mongoService.GetBrewery(id);

            return View(brewery);
        }

        // POST: Brewery/Edit/id
        [HttpPost]
        [Authorize]
        public ActionResult Edit(string id, Brewery brewery)
        {
            try
            {
                var result = mongoService.updateBrewery(id, brewery);

                if (result.ModifiedCount > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Something went wrong, failed to update.";

                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Brewery/Delete
        [BrewAuthorize]
        public ActionResult Delete(string id)
        {
            var brewery = mongoService.GetBrewery(id);

            return View(brewery);
        }

        // POST: Brewery/Delete
        [HttpPost]
        [BrewAuthorize]
        public ActionResult Delete(string id, Brewery brewery)
        {
            try
            {
                var result = mongoService.deleteBrewery(id);

                if (result.DeletedCount > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Something went wrong, failed to delete.";

                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
    }
}
