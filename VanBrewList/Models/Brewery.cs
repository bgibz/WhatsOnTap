using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using VanBrewList.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VanBrewList.Models
{
    public class Brewery
    { 
        [BsonId]
        public ObjectId _id { get; set; }

        [Required]
        [BsonElement("Name")]
        public string Name { get; set;}
        [BsonElement("Address")]
        public string Address { get; set; }
        [BsonElement("Growlers")]
        public ICollection<Beer> Growlers { get; set; }
        [BsonElement("TastingRoom")]
        public ICollection<Beer> TastingRoom { get; set; }
    }
}