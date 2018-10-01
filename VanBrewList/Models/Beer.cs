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

        public string Img { get; set; }

        public void setImg()
        {
            string lStyle = Style.ToLower();
            string lName = Name.ToLower();
            
            if (lStyle.Contains("porter") || lName.Contains("porter"))
            {
                Img = "../../img/stout.svg";
            }
            else if (lStyle.Contains("hefe") || lName.Contains("hefe") || lStyle.Contains("wheat") || lName.Contains("wheat"))
            {
                Img = "../../img/wheat.svg";
            }
            else if (lStyle.Contains("ipa") || lName.Contains("ipa"))
            {
                Img = "../../img/ipa.svg";
            }
            else if (lStyle.Contains("ale") || lName.Contains("ale"))
            {
                Img = "../../img/mug.svg";
            }
            else
            {
                Img = "../../img/pint.svg";
            }

        }
    }
}