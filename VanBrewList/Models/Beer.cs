using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VanBrewList.Models
{
    public class Beer
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [Required]
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Style")]
        public string Style { get; set; }

        [BsonElement("Abv")]
        public int Abv { get; set; }

        [Required]
        [BsonElement("Brewery")]
        public Brewery BrewedBy { get; set; }

    }
}