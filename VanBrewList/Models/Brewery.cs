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
        [BsonElement("name")]
        public string Name { get; set;}
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("url")]
        public string Url { get; set; }
        [BsonElement("growlers")]
        public ICollection<Beer> Growlers { get; set; }
        [BsonElement("tasting_room")]
        public ICollection<Beer> TastingRoom { get; set; }
    }
}