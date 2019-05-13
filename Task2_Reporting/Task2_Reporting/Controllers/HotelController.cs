using System;
using ExcelLibrary.SpreadSheet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Task2_Reporting.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index(string jsontxt)
        {
            if (!string.IsNullOrEmpty(jsontxt))
            {
                // string to dynamic JSON object
                var hotel = JsonConvert.DeserializeObject<dynamic>(jsontxt).hotelRates;
                string file = @".\Report.xls";
                // Creating a new websheet
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("Rooms");

                // Adjusting width of the columns
                for (int i = 0; i < 7; i++)
                {
                    worksheet.Cells.ColumnWidth[0, (ushort)i] = 3000;
                }
                // Setting header row
                worksheet.Cells[0, 0] = new Cell("ARRIVAL_DATE");
                worksheet.Cells[0, 1] = new Cell("DEPARTURE_DATE");
                worksheet.Cells[0, 2] = new Cell("PRICE");
                worksheet.Cells[0, 3] = new Cell("CURRENCY");
                worksheet.Cells[0, 4] = new Cell("RATENAME");
                worksheet.Cells[0, 5] = new Cell("ADULTS");
                worksheet.Cells[0, 6] = new Cell("BREAKFAST_INCLUDED");

                // Populating columns
                var index = 1;
                foreach (var room in hotel)
                {
                    var dateTime = DateTime.Parse(room.targetDay.ToString());
                    worksheet.Cells[index, 0] = new Cell(dateTime.ToString("dd.MM.yy"));
                    worksheet.Cells[index, 1] = new Cell(dateTime.AddDays(int.Parse(room.los.ToString())).ToString("dd.MM.yy"));
                    worksheet.Cells[index, 2] = new Cell(room.price.numericFloat.ToString());
                    worksheet.Cells[index, 3] = new Cell(room.price.currency.ToString());
                    worksheet.Cells[index, 4] = new Cell(room.rateName.ToString());
                    worksheet.Cells[index, 5] = new Cell(room.adults.ToString());
                    worksheet.Cells[index, 6] = new Cell(bool.Parse(room.rateTags[0].shape.ToString()) ? "1" : "0");
                    index++;
                }
                // creating a report file 
                workbook.Worksheets.Add(worksheet);
                workbook.Save(file);
            }
            // Notification flag
            ViewBag.fileCreated = !string.IsNullOrEmpty(jsontxt);
            return View();

        }
    }
}