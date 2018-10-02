using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using VanBrewList.Models;
using VanBrewList.Services;

namespace VanBrewList.Controllers
{
    public class BeerController : Controller
    {

        private MongoService mongoService;

        public BeerController()
        {
            this.mongoService = new MongoService();
        }

        // GET: Beer
        public ActionResult Growlers()
        {
            var beers = mongoService.getGrowlers();

            return View(beers);
        }

        // GET: Beer
        public ActionResult TastingRooms()
        {
            var beers = mongoService.getTastingRooms();

            return View(beers);
        }

        // GET: Beer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Beer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Beer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Beer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Beer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Beer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Beer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
