using System;
using System.Collections.Generic;
using System.Text;

namespace Task1_WebExtraction
{
    class Hotel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        //public string Stars { get; set; }
        public string ReviewPoints { get; set; }
        public string NoOfReviews { get; set; }
        public List<string> RoomCategories { get; set; }
        public string Description { get; set; }

        public List<Alternative> Alternatives { get; set; }

        public Hotel()
        {
            RoomCategories = new List<string>();
            Alternatives = new List<Alternative>();
        }
    }
}
