using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanBrewList.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace VanBrewList.Controllers
{
    public class BreweryController : Controller
    {
        private IMongoDatabase db;

        public BreweryController()
        {
            string uri = "mongodb://vanbrewuser:VbDB18@ds018538.mlab.com:18538/vanbrewalpha";
            var client = new MongoClient(uri);
            this.db = client.GetDatabase("vanbrewalpha");
        }

        // GET: Brewery
        public ActionResult Index()
        {
            var breweries = db.GetCollection<Brewery>("breweries");
            ICollection<Brewery> brewList = breweries.Find(_ => true).ToList();

            return View("Index", brewList);
        }

        // GET: Brewery/Details/5
        public ActionResult Details(string id)
        {

            var brewId = new ObjectId(id);

            var breweries = db.GetCollection<Brewery>("breweries");
            var brewery = breweries.Find(b => b._id == brewId).FirstOrDefault();

            return View("Detail", brewery);
        }

        // GET: Brewery/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brewery/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var breweries = db.GetCollection<BsonDocument>("breweries");
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brewery/Edit/5
        public ActionResult Edit(string id)
        {
            var brewId = new ObjectId(id);

            var breweries = db.GetCollection<Brewery>("breweries");
            var brewery = breweries.Find(b => b._id == brewId).FirstOrDefault();

            return View(brewery);
        }

        // POST: Brewery/Edit/5
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

        // GET: Brewery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brewery/Delete/5
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
