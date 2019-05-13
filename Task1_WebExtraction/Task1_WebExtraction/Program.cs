using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Task1_WebExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            GetHtmlAsync();
            Console.ReadLine();
        }

        private static void GetHtmlAsync()
        {
            try
            {
                /*
                 * - Extracting text from HTML file
                 * - Populating hotel object
                 */
                #region Scraping
                var htmlDocument = new HtmlDocument();
                htmlDocument.Load(@"..\..\..\src\task 1 - Kempinski Hotel Bristol Berlin, Germany - Booking.com.html");

                Hotel hotel = new Hotel();
                hotel.Name = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='hp_hotel_name']").InnerText;
                hotel.Address = htmlDocument.DocumentNode.SelectSingleNode("//*[@id='hp_address_subtitle']").InnerText;
                hotel.ReviewPoints = htmlDocument.DocumentNode.SelectSingleNode("//span[@class='average js--hp-scorecard-scoreval']").InnerText;
                hotel.NoOfReviews = htmlDocument.DocumentNode.SelectSingleNode("//strong[@class='count']").InnerText;
                var alternative = htmlDocument.DocumentNode.SelectSingleNode("//tr[@id='althotelsRow']").Descendants("td");

                foreach (var alt in alternative)
                {
                    Alternative suggestedHotel = new Alternative();
                    suggestedHotel.Name = alt.SelectSingleNode("//a[@class='althotel_link']").InnerText;
                    suggestedHotel.Link = alt.SelectSingleNode("//a[@class='althotel_link']").Attributes["href"].Value;
                    suggestedHotel.NoOfReviews = alt.SelectSingleNode("//strong[@class='count']").InnerText;
                    suggestedHotel.Description = alt.SelectSingleNode("//span[@class='hp_compset_description']").InnerText;
                    suggestedHotel.ReviewPoints = alt.SelectSingleNode("//span[@class='average js--hp-scorecard-scoreval']").InnerText;
                    hotel.Alternatives.Add(suggestedHotel);
                }

                var roomTypes = htmlDocument.DocumentNode.SelectSingleNode("//table[@id='maxotel_rooms']").SelectNodes("//td[@class='ftd']");
                foreach (var room in roomTypes)
                {
                    hotel.RoomCategories.Add(room.InnerText);
                }

                var discription = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='summary']").Descendants("p");
                foreach (var para in discription)
                {
                    hotel.Description += para.InnerText;
                }
                #endregion

                /*
                 * - Serializing hotel object
                 * - Beautification of outpu
                */
                #region Object to JSON
                string hotelSerialize = JsonConvert.SerializeObject(hotel);
                Newtonsoft.Json.Linq.JToken parsedJson = Newtonsoft.Json.Linq.JToken.Parse(hotelSerialize);
                var beautified = parsedJson.ToString(Formatting.Indented);
                Console.WriteLine(beautified.Replace("\\n", ""));
                #endregion
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }
        }
    }
}
