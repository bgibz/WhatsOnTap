﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
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
        public ActionResult Create()
        {
            NewBeer viewModel = new NewBeer();

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
        public ActionResult Create(NewBeer beer)
        {
            try
            {
                Beer newBeer = new Beer();
                newBeer.Name = beer.Name;
                newBeer.Style = beer.Style;
                newBeer.Abv = beer.Abv;
                
                // get brewery by ID

                // Add beer to brewery

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
