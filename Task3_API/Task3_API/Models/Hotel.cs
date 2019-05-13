using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task3_API.Models
{
    public class Hotel
    {
        public int classification { get; set; }
        public int hotelID { get; set; }
        public string name { get; set; }
        public double reviewscore { get; set; }
    }
}
