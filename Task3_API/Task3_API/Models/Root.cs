using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task3_API.Models
{
    public class Root
    {
        public Hotel hotel { get; set; }
        public List<HotelRate> hotelRates { get; set; }
    }
}
