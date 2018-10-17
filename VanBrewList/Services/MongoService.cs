
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using VanBrewList.Models;

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

        public IMongoDatabase getContext()
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

        public ICollection<Beer> getGrowlers()
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

        public ICollection<Beer> getTastingRooms()
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

        public UpdateResult updateBrewery(string id, Brewery brewery)
        {
            var brewId = new ObjectId(id);
            var collection = db.GetCollection<Brewery>("breweries");

            var filter = Builders<Brewery>.Filter.Eq("_id", brewId);
            var update = Builders<Brewery>.Update.Set("address", brewery.Address)
                .Set("name", brewery.Name)
                .Set("url", brewery.Url);

            return collection.UpdateOne(filter, update);
        }

        public bool checkBrewName(string name)
        {
            var collection = db.GetCollection<Brewery>("breweries");
            var check = collection.Find(b => b.Name == name).FirstOrDefault();

            if (check != null) return false;
            return true;
        }

        public void createBrewery(Brewery brewery)
        {
            var collection = db.GetCollection<Brewery>("breweries");
            collection.InsertOne(brewery);
        }

        public DeleteResult deleteBrewery(string id)
        {
            var brewId = new ObjectId(id);
            var collection = db.GetCollection<Brewery>("breweries");

            var filter = Builders<Brewery>.Filter.Eq("_id", brewId);
            var delete = collection.DeleteOne(filter);
            return delete;
        }
    }
}