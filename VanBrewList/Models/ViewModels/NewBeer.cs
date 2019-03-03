using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VanBrewList.Models.ViewModels
{
    public class NewBeer
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Style { get; set; }

        [Display(Name = "ABV")]
        public string Abv { get; set; }
        [Display(Name = "Available for Growler fills")]
        public bool Growler { get; set; }
        [Display(Name = "Available in Tasting Room")]
        public bool TastingRoom { get; set; }

        [Display(Name = "Brewery")]
        public IEnumerable<SelectListItem> breweries { get; set; }

        public string id { get; set; }
    }
}