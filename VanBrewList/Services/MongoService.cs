﻿
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using VanBrewList.Models;
using VanBrewList.Models.ViewModels;

namespace VanBrewList.Services
{
    public class MongoService
    {
        private IMongoDatabase db;

        public MongoService()
        {
            string uri = ConfigurationManager.AppSettings["MongoURI"];
            var client = new MongoClient(uri);
            this.db = client.GetDatabase("vanbrewalpha");
        }

        public IMongoDatabase GetContext()
        {
            return this.db;
        }

        public IMongoCollection<Admin> getAdminTable()
        {
            var collection = db.GetCollection<Admin>("user");
            return collection;
        }

        public ICollection<Brewery> GetBreweries()
        {
            var collection = db.GetCollection<Brewery>("breweries");
            ICollection<Brewery> breweries = collection.Find(_ => true).ToList();
            return breweries;
        }

        public Brewery GetBrewery(string id)
        {
            var brewId = new ObjectId(id);
            var collection = db.GetCollection<Brewery>("breweries");
            var brewery = collection.Find(b => b._id == brewId).FirstOrDefault();

            return brewery;
        }

        public ICollection<Beer> GetGrowlers()
        { 
            ICollection<Beer> beers = new List<Beer>();
            var collection = db.GetCollection<Brewery>("breweries");

            ICollection<Brewery> brewList = collection.Find(_ => true).ToList();
            foreach (Brewery br in brewList)
            {
                foreach (Beer b in br.Growlers)
                {
                    b.Brewery = br;
                    if (!beers.Contains(b)) beers.Add(b);
                }
            }

            return beers;
        }

        public ICollection<Beer> GetTastingRooms()
        {
            ICollection<Beer> beers = new List<Beer>();
            var collection = db.GetCollection<Brewery>("breweries");
            ICollection<Brewery> brewList = collection.Find(_ => true).ToList();
            foreach (Brewery br in brewList)
            {
                foreach (Beer b in br.TastingRoom)
                {
                    b.Brewery = br;
                    if (!beers.Contains(b)) beers.Add(b);
                }
            }

            return beers;
        }

        public UpdateResult UpdateBrewery(string id, Brewery brewery)
        {
            var brewId = new ObjectId(id);
            var collection = db.GetCollection<Brewery>("breweries");

            var filter = Builders<Brewery>.Filter.Eq("_id", brewId);
            var update = Builders<Brewery>.Update.Set("address", brewery.Address)
                .Set("name", brewery.Name)
                .Set("url", brewery.Url);

            return collection.UpdateOne(filter, update);
        }

        public bool CheckBrewName(string name)
        {
            var collection = db.GetCollection<Brewery>("breweries");
            var check = collection.Find(b => b.Name == name).FirstOrDefault();

            if (check != null) return false;
            return true;
        }

        public void CreateBrewery(Brewery brewery)
        {
            var collection = db.GetCollection<Brewery>("breweries");
            collection.InsertOne(brewery);
        }

        public DeleteResult DeleteBrewery(string id)
        {
            var brewId = new ObjectId(id);
            var collection = db.GetCollection<Brewery>("breweries");

            var filter = Builders<Brewery>.Filter.Eq("_id", brewId);
            var delete = collection.DeleteOne(filter);
            return delete;
        }

        public UpdateResult AddBeerGrowler(BeerView beer)
        {
            var brewId = new ObjectId(beer.id);
            var newName = beer.Name;
            var newStyle = beer.Style;
            var newAbv = beer.Abv;
            var collection = db.GetCollection<Brewery>("breweries");
            var filter = Builders<Brewery>.Filter.Eq("_id", brewId);
            var update = Builders<Brewery>.Update.AddToSet("growlers", new Beer
            {
                Url = "",
                Name = beer.Name,
                Style = beer.Style,
                Abv = beer.Abv,
            });
            return collection.UpdateOne(filter, update);
        }

        public UpdateResult AddBeerTastingRoom(BeerView beer)
        {
            var brewId = new ObjectId(beer.id);
            var newName = beer.Name;
            var newStyle = beer.Style;
            var newAbv = beer.Abv;
            var collection = db.GetCollection<Brewery>("breweries");
            var filter = Builders<Brewery>.Filter.Eq("_id", brewId);
            var update = Builders<Brewery>.Update.AddToSet("tasting_room", new Beer
            {
                Url = "",
                Name = beer.Name,
                Style = beer.Style,
                Abv = beer.Abv,
            });
            return collection.UpdateOne(filter, update);
        }

        public UpdateResult DeleteBeerGrowler(BeerView beer)
        {
            var breweryID = new ObjectId(beer.id);
            var collection = db.GetCollection<Brewery>("breweries");
            var filter = Builders<Brewery>.Filter.Eq("_id", breweryID);
            var update = Builders<Brewery>.Update.Pull("growlers", new Beer
            {
                Url = beer.Url,
                Name = beer.Name,
                Style =beer.Style,
                Abv = beer.Abv,
            });
            return collection.UpdateOne(filter, update);
        }

        public UpdateResult DeleteBeerTastingRoom(BeerView beer)
        {
            var breweryID = new ObjectId(beer.id);
            var collection = db.GetCollection<Brewery>("breweries");
            var filter = Builders<Brewery>.Filter.Eq("_id", breweryID);
            var update = Builders<Brewery>.Update.Pull("growlers", new Beer
            {
                Url = beer.Url,
                Name = beer.Name,
                Style = beer.Style,
                Abv = beer.Abv,
            });
            return collection.UpdateOne(filter, update);
        }
    }
}