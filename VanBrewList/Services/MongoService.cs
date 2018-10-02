
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
    }
}