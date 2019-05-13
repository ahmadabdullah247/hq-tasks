using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Task3_API.Models;

namespace Task3_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : Controller
    {
        // GET api/values
        [HttpGet]
        public JsonResult Get([FromQuery] int HotelID,[FromQuery]string ArrivalDate)
        {
            // Reading and serializing JSON file according to defined Models
            var streamReader = new StreamReader(@".\hotelsrates.json");
            var jsonTxt = streamReader.ReadToEnd();
            var hotels = JsonConvert.DeserializeObject<List<Root>>(jsonTxt);
            ArrivalDate = ArrivalDate.Remove(ArrivalDate.Length - 6);
            DateTime dateval;

            // Check all permutations of query parameters
            if ((HotelID > 0) && (!string.IsNullOrEmpty(ArrivalDate))&&(DateTime.TryParse(ArrivalDate,out dateval)))
            {
                hotels = hotels.Where(x => x.hotel.hotelID == HotelID).ToList();
                var arrivalDate = DateTime.Parse(ArrivalDate.Remove(ArrivalDate.Length - 6));
                foreach (var hotel in hotels)
                {
                    hotel.hotelRates = hotel.hotelRates.Where(y => y.targetDay.Date == arrivalDate.Date).ToList();
                }
                return Json(hotels);
            }
            else if (HotelID > 0)
            {
                return Json(hotels.Where(x => x.hotel.hotelID == HotelID));
            }
            else if (!string.IsNullOrEmpty(ArrivalDate) && (DateTime.TryParse(ArrivalDate, out dateval)))
            {
                var arrivalDate = DateTime.Parse(ArrivalDate);
                foreach (var hotel in hotels)
                {
                    hotel.hotelRates = hotel.hotelRates.Where(y => y.targetDay.Date == arrivalDate.Date).ToList();
                }
                return Json(hotels);
            }
            else
            {
                return Json(hotels);
            }
        }

  
    }
}