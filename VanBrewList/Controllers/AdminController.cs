﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VanBrewList.Models;
using VanBrewList.Services;

namespace VanBrewList.Controllers
{
    public class AdminController : Controller
    {
        private MongoService mongoService;

        public AdminController()
        {
            this.mongoService = new MongoService();
        }

        public ActionResult Index()
        {
            AdminIndex viewModel = new AdminIndex();

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

        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin user)
        {
            if (ModelState.IsValid)
            {
                IMongoCollection<Admin> users = mongoService.getAdminTable();
                var admin = users.Find(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (admin != null)
                {
                    Session["Username"] = admin.Username;
                    Session["Role"] = "Admin";
                    return RedirectToAction("Index", "Home", null);
                }
            }
            TempData["Error"] = "Login failed";
            return View();
        }

    }
}
