using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VanBrewList.Models
{
    public class Beer
    {
        [Required]
        public string name { get; set; }

        public string style { get; set; }

        public int abv { get; set; }

        [Required]
        public Brewery brewedBy { get; set; }

    }
}