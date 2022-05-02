using CoolBooks.Data;
using CoolBooks.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace CoolBooks.Controllers
{
    public class GraphController : Controller
    {
        //---------------------------------------------------------------------------------
        //8. Det skall finnas en grafisk uppföljning av antal kommentarer per dag och vecka,
        //dels totalt, dels per genre och förfatrare.
        //Använd samma grafiska API som vi körde under PHP kursen
        //alternativt välj eget med samma funktionalitet
        //---------------------------------------------------------------------------------
        private readonly CoolbooksContext _context;
		


		public GraphController(CoolbooksContext context)
		{
			_context = context;
			
			
		}
		public ActionResult Index()
		{
            //List<DataPoint> dataPoints = new List<DataPoint>();


            //dataPoints.Add(new DataPoint("Great Britain", 67));
            //dataPoints.Add(new DataPoint("China", 70));
            //dataPoints.Add(new DataPoint("Russia", 56));
            //dataPoints.Add(new DataPoint("Germany", 42));
            //dataPoints.Add(new DataPoint("Japan", 41));
            //dataPoints.Add(new DataPoint("France", 42));
            //dataPoints.Add(new DataPoint("South Korea", 21));

            //ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);


            var coolbooksContext = _context.Authors
                .Select(p => new DataPoint
                {
                    Label = p.FullName,
                    Y = p.BooksFromAutors.Count(),
                   
                })
                .ToList();


            ViewBag.DataPoints = JsonConvert.SerializeObject(coolbooksContext);
            return View();

		}
		public ActionResult AddedBooks()
        {
            var coolbooksContext = _context.Authors
                   .Select(p => new DataPoint
                   {
                       Label = p.FullName,
                       Y = p.BooksFromAutors.Count(),

                   })
                   .ToList();


            ViewBag.DataPoints = JsonConvert.SerializeObject(coolbooksContext);
            return View();
        }

		public ActionResult HighestRatedBooks()
        {
			return View();
        }
		public ActionResult MostCommented()
        {
			return View();
        }
	}
}
