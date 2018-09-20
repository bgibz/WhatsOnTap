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
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("style")]
        public string Style { get; set; }

        [BsonElement("abv")]
        public string Abv { get; set; }

        [Required]
        [BsonElement("url")]
        public string Url { get; set; }

    }
}