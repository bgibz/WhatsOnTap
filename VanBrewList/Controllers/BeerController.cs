using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace VanBrewList.Controllers
{
    public class BeerController : Controller
    {

        private IMongoDatabase db;

        public BeerController()
        {
            string uri = "mongodb://vanbrewuser:VbDB18@ds018538.mlab.com:18538/vanbrewalpha";
            var client = new MongoClient(uri);
            this.db = client.GetDatabase("vanbrewalpha");
        }

        // GET: Beer
        public ActionResult Index()
        {


            return View();
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
