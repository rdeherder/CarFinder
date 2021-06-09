using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderUI.Library.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int HP { get; set; }
        public double Price { get; set; }
        public string Img_Url { get; set; }
    }
}
