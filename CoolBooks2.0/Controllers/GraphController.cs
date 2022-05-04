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

           


            var test2 =
                                        
                                       from r in _context.Reviews
                                       join b in _context.Books on r.BookID equals b.BooksID
                                       join ba in _context.BooksAuthors on b.BooksID equals ba.BooksID
                                       join a in _context.Authors on ba.AuthorID equals a.AuthorID
                                       select new 
                                       {
                                           Label = a.FirstName + " " + a.LastName,
                                           Y = r.Created
                                       };
                  var test4 = test2.GroupBy(b=>  new { b.Label, b.Y })
                .Select(g => new DataPoint
                {
                    Label= g.Key.Label,
                    Y = g.Count()
                }).ToList();

            //var test3 =
            //                           from a in _context.Authors
            //                           join ba in _context.BooksAuthors on a.AuthorID equals ba.AuthorID
            //                           join b in _context.Books on ba.BooksID equals b.BooksID
            //                           join r in _context.Reviews on b.BooksID equals r.BookID
            //                           group a.FullName by new { r.Created, a.FullName} into g
            //                           select new DataPoint
            //                           {
            //                               Label = g.Key.FullName.ToString(),
            //                               Y = g.Count()
            //                           };

            //var coolbooksContext = _context.Reviews
            //    .GroupBy(g=>g.Created)
            //    .Select(p => new DataPoint
            //    {
            //        Label = p.Key.ToString(),
            //        Y = p.Count(),
                   
            //    })
            //    .ToList();


            ViewBag.DataPoints = JsonConvert.SerializeObject(test4);
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
