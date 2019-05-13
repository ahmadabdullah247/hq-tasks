using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task3_API.Models
{
    public class HotelRate
    {
        public int adults { get; set; }
        public int los { get; set; }
        public Price price { get; set; }
        public string rateDescription { get; set; }
        public string rateID { get; set; }
        public string rateName { get; set; }
        public List<RateTag> rateTags { get; set; }
        public DateTime targetDay { get; set; }
    }
}
