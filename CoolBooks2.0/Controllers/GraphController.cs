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
        //8. Det skall finnas en grafisk uppföljning av antal rewviews per dag och vecka,
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
            
            //var pastDate = DateTime.Now.Date.AddDays(-7);
            //var coolbooksContext = _context.Reviews
                
            //    .GroupBy(g=> new { g.Created, g.Title})
                
            //    .Where(g =>g.Key.Created > pastDate)
            //       .Select (p => new DataPoint
            //       {
            //           Label = p.Key.Title.ToString(),
                       
            //           Y = p.Count(),

            //       })
            //       .ToList();


            //ViewBag.DataPoints = JsonConvert.SerializeObject(coolbooksContext);

            return View();

		}
		public ActionResult MostCommentedBook1day()
        {
            var pastDate = DateTime.Now.Date.AddDays(-1);
            var coolbooksContext = _context.Reviews
                .GroupBy(g => new { g.Created, g.Title })
                .Where(g => g.Key.Created > pastDate)
                   .Select(p => new DataPoint
                   {
                       Label = p.Key.Title.ToString(),

                       Y = p.Count(),

                   })
                   .ToList();


            ViewBag.DataPoints = JsonConvert.SerializeObject(coolbooksContext);


            return View();
        }

        public ActionResult MostCommentedAuthorTotal()
        {
            //var pastDate = DateTime.Now.Date.AddDays(-7);
            var query =

              from r in _context.Reviews
              join b in _context.Books on r.BookID equals b.BooksID
              join ba in _context.BooksAuthors on b.BooksID equals ba.BooksID
              join a in _context.Authors on ba.AuthorID equals a.AuthorID
              //where (r.Created > pastDate)
              select new
              {
                 Label = a.FirstName + " " + a.LastName + Environment.NewLine + r.Created, //line break sql?
                 Y = r.Created
              };
            var GraphInput = query.GroupBy(b => new { b.Label, b.Y })
            .Select(g => new DataPoint
            {
                Label = g.Key.Label,
                Y = g.Count()
            })
            .ToList();

            ViewBag.DataPoints = JsonConvert.SerializeObject(GraphInput);
            return View();

            return View();
        }

        public ActionResult MostCommentedAuthor7days()
        {
            var pastDate = DateTime.Now.Date.AddDays(-7);
            var query =

                                       from r in _context.Reviews
                                       join b in _context.Books on r.BookID equals b.BooksID
                                       join ba in _context.BooksAuthors on b.BooksID equals ba.BooksID
                                       join a in _context.Authors on ba.AuthorID equals a.AuthorID
                                       where(r.Created > pastDate)
                                       select new
                                       {
                                           Label = a.FirstName + " " + a.LastName + Environment.NewLine + r.Created, //line break sql?
                                           Y = r.Created
                                       };
                                        var GraphInput = query.GroupBy(b => new { b.Label, b.Y })
                                        .Select(g => new DataPoint
                                            {
                                                Label = g.Key.Label,
                                                Y = g.Count()
                                            })
                                        .ToList();

            ViewBag.DataPoints = JsonConvert.SerializeObject(GraphInput);
            return View();
        }


        public ActionResult MostCommentedAuthor1days()
        {
            var pastDate = DateTime.Now.Date.AddDays(-1);
            var query =

                from r in _context.Reviews
                join b in _context.Books on r.BookID equals b.BooksID
                join ba in _context.BooksAuthors on b.BooksID equals ba.BooksID
                join a in _context.Authors on ba.AuthorID equals a.AuthorID
                where (r.Created > pastDate)
                select new
                {
                    Label = a.FirstName + " " + a.LastName + " " + r.Created, //line break sql?
                    Y = r.Created
                };

            var GraphInput = query.GroupBy(b => new { b.Label, b.Y })
            .Select(g => new DataPoint
            {
                Label = g.Key.Label,
                Y = g.Count()
            })
            .ToList();

            ViewBag.DataPoints = JsonConvert.SerializeObject(GraphInput);
            return View();
        }
        public ActionResult MostCommentedGenresTotal()

        {
            var pastDate = DateTime.Now.Date.AddDays(-7);
            var query = from r in _context.Reviews
                        join b in _context.Books on r.BookID equals b.BooksID
                        join bg in _context.BooksGenres on b.BooksID equals bg.BooksID
                        join g in _context.Genres on bg.GenreID equals g.GenreID
                        where (r.Created > pastDate)
                        select new
                        {
                            Label = g.Name + " " + r.Created, //line break sql?

                            Y = r.Created
                        };
            var GraphInput = query.GroupBy(b => new { b.Label, b.Y })
            .Select(g => new DataPoint
            {
                Label = g.Key.Label,
                Y = g.Count()
            })
            .ToList();
            ViewBag.DataPoints = JsonConvert.SerializeObject(GraphInput);
            return View();
        }

        public ActionResult MostCommentedGenres7days()
        {
            var pastDate = DateTime.Now.Date.AddDays(-7);
            var query = from r in _context.Reviews
                        join b in _context.Books on r.BookID equals b.BooksID
                        join bg in _context.BooksGenres on b.BooksID equals bg.BooksID
                        join g in _context.Genres on bg.GenreID equals g.GenreID
                        where (r.Created > pastDate)
                        select new
                        {
                            Label = g.Name + " " + r.Created, //line break sql?
                            
                            Y = r.Created
                        };
            var GraphInput = query.GroupBy(b => new { b.Label, b.Y })
            .Select(g => new DataPoint
            {
                Label = g.Key.Label,
                Y = g.Count()
            })
            .ToList();
            ViewBag.DataPoints = JsonConvert.SerializeObject(GraphInput);
            return View();
        }

        public ActionResult MostCommentedGenresToday()
        {
            var pastDate = DateTime.Now.Date.AddDays(-1);
            var query = from r in _context.Reviews
                        join b in _context.Books on r.BookID equals b.BooksID
                        join bg in _context.BooksGenres on b.BooksID equals bg.BooksID
                        join g in _context.Genres on bg.GenreID equals g.GenreID
                        where (r.Created > pastDate)
                        select new
                        {
                            Label = g.Name + " " + r.Created, //line break sql?

                            Y = r.Created
                        };
            var GraphInput = query.GroupBy(b => new { b.Label, b.Y })
            .Select(g => new DataPoint
            {
                Label = g.Key.Label,
                Y = g.Count()
            })
            .ToList();
            ViewBag.DataPoints = JsonConvert.SerializeObject(GraphInput);
            return View();
        }
    }
}
