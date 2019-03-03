using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VanBrewList.Models
{
    public class AdminIndex
    {
        public IEnumerable<SelectListItem> breweries { get; set; }

        public string id { get; set; }
    }
}