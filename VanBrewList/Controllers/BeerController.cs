using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using VanBrewList.Authorize;
using VanBrewList.Models;
using VanBrewList.Models.ViewModels;
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
            var beers = mongoService.GetGrowlers();

            return View(beers);
        }

        // GET: Beer
        public ActionResult TastingRooms()
        {
            var beers = mongoService.GetTastingRooms();

            return View(beers);
        }

        // GET: Beer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Beer/Create
        [BrewAuthorize]
        public ActionResult Create()
        {
            BeerView viewModel = new BeerView();

            var breweries = mongoService.GetBreweries();

            List<SelectListItem> breweryList = new List<SelectListItem>();

            foreach (Brewery brewery in breweries)
            {
                SelectListItem b = new SelectListItem() { Text = brewery.Name, Value = brewery._id.ToString() };
                breweryList.Add(b);
            }

            viewModel.breweries = breweryList;

            return View(viewModel);
        }

        // POST: Beer/Create
        [HttpPost]
        [BrewAuthorize]
        public ActionResult Create(BeerView beer)
        {
            try
            {
                if (beer.Growler && beer.TastingRoom)
                {
                    var result1 = mongoService.AddBeerGrowler(beer);
                    var result2 = mongoService.AddBeerTastingRoom(beer);
                }
                else if (beer.Growler)
                {
                    var result = mongoService.AddBeerGrowler(beer);
                }
                else if (beer.TastingRoom)
                {
                    var result = mongoService.AddBeerTastingRoom(beer);
                }

                Brewery brewery = mongoService.GetBrewery(beer.id);

                return RedirectToAction("Index", "Brewery");
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
            BeerView beer = new BeerView();

            return View();
        }

        // POST: Beer/Delete/5
        [HttpPost]
        [BrewAuthorize]
        public ActionResult Delete(BeerView beer)
        {
            try
            {
                if (beer.Growler && beer.TastingRoom)
                {
                    var result1 = mongoService.DeleteBeerGrowler(beer);
                    var result2 = mongoService.DeleteBeerTastingRoom(beer);
                }
                else if (beer.Growler)
                {
                    var result = mongoService.DeleteBeerGrowler(beer);
                }
                else if (beer.TastingRoom)
                {
                    var result = mongoService.DeleteBeerTastingRoom(beer);
                }

                Brewery brewery = mongoService.GetBrewery(beer.id);

                return RedirectToAction("Index", "Brewery");
            }
            catch
            {
                return View();
            }
        }
    }
}
