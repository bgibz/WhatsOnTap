using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using VanBrewList.Models;
using MongoDB.Bson;

namespace VanBrewList.Models
{
    public class Brewery
    { 
        [Key]
        public ObjectId _id { get; set; }

        [Required]
        public string Name { get; set;}

        public string Address { get; set; }

        public ICollection<Beer> Growlers { get; set; }

        public ICollection<Beer> TastingRoom { get; set; }
    }
}