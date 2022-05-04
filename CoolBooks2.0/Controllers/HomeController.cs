using CoolBooks.Areas.Identity;
using CoolBooks.Data;
using CoolBooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace CoolBooks.Controllers
{
    public class HomeController : Controller
    {
        private readonly CoolbooksContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public HomeController(CoolbooksContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private List<int> RandomBooks()
        {
            var books = _context.Books.ToList();
            var excludedBooks = books.Where(x => x.IsBookOfTheWeek || x.IsDeleted || x.MostCommetedBook || x.MostDislikedBook || x.MostLikedBook).Select(y => y.BooksID).ToList();
            Random rnd = new Random();

            List<int> RandomBooklist = new List<int>();
            while (RandomBooklist.Count() < 6)
            {
                var randomNumber = rnd.Next(1, books.Count());

                if (!excludedBooks.Contains(randomNumber))
                {
                    if (!RandomBooklist.Contains(randomNumber))
                    {
                        RandomBooklist.Add(randomNumber);
                    }

                }

            }
            return RandomBooklist;
        }

        private List<BooksViewModel> GetAllBooks()
        {
            var coolbooksContext = _context.Books
                .Select(p => new BooksViewModel
                {
                    BooksID = p.BooksID,
                    Title = p.Title,
                    Description = p.Description,
                    ISBN = p.ISBN,
                    ImagePath = p.ImagePath,
                    Created = p.Created,
                    IsBookOfTheWeek = p.IsBookOfTheWeek,
                    MostCommetedBook = p.MostCommetedBook,
                    MostLikedBook = p.MostLikedBook,
                    MostDislikedBook = p.MostDislikedBook,
                    NewestBook = p.NewestBook,
                    GenreName = (List<string>)p.GenresFromBooks.Select(m => m.Genre.Name),
                    AuthorName = (List<string>)p.AuthorsFromBooks.Select(m => m.Author.FullName),
                    UserName = (List<string>)p.BooksUsers.Select(m => m.Client.UserName),
                    Reviews = p.Reviews.ToList(),
                    RatingAvg = p.Reviews.Select(m => m.Rating).Average(),
                })
                .ToList();

            return coolbooksContext;
        }

        private void MostReviewedBook()
        {
            var mostReviews = GetAllBooks().GroupBy(x => new { x.Reviews, x.BooksID })
               .Select(y => new
               {
                   BooksID = y.Key.BooksID,
                   NumberOfReviews = y.Key.Reviews.Count()
               });

            var mostReviewsBookId = mostReviews.OrderByDescending(x => x.NumberOfReviews)
                .Select(y => y.BooksID).FirstOrDefault();


            var books = _context.Books;
                        
            foreach (var book in books)
            {
                book.MostCommetedBook = false;
                if (book.BooksID == mostReviewsBookId)
                {
                    book.MostCommetedBook = true;
                }
                _context.Books.Update(book);
            }

            _context.SaveChanges();

        }

        private void MostLikedBook()
        {
           
            var mostLiked = GetAllBooks().GroupBy(x => new { x.RatingAvg, x.BooksID })
                .Where(x=>x.Key.RatingAvg != null)
               .Select(y => new
               {
                   BooksID = y.Key.BooksID,
                   RatingSum = y.Key.RatingAvg
               });

            var mostLikedBookId = mostLiked.OrderByDescending(x => x.RatingSum)
                .Select(y => y.BooksID).FirstOrDefault();


            var books = _context.Books;

            foreach (var book in books)
            {
                book.MostLikedBook = false;
                if (book.BooksID == mostLikedBookId)
                {
                    book.MostLikedBook = true;
                }
                _context.Books.Update(book);
            }

            _context.SaveChanges();

        }
        private void MostDisLikedBook()
        {

            var mostDisLiked = GetAllBooks().GroupBy(x => new { x.RatingAvg, x.BooksID })
                .Where(x => x.Key.RatingAvg != null)
               .Select(y => new
               {
                   BooksID = y.Key.BooksID,
                   RatingSum = y.Key.RatingAvg
               });

            var mostDisLikedBookId = mostDisLiked.OrderBy(x => x.RatingSum)
                .Select(y => y.BooksID).FirstOrDefault();


            var books = _context.Books;

            foreach (var book in books)
            {
                book.MostLikedBook = false;
                if (book.BooksID == mostDisLikedBookId)
                {
                    book.MostLikedBook = true;
                }
                _context.Books.Update(book);
            }

            _context.SaveChanges();

        }

        public IActionResult Index()
        {
            MostReviewedBook();
            MostLikedBook();
            MostDisLikedBook();
            ViewData["Random"] = RandomBooks();
            

            return View(GetAllBooks());
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Books()
        {
            return View();
        }
        public IActionResult Reviews()
        {
            return View();
        }

        public IActionResult Authors()
        {
            return View();
        }

        public IActionResult Genres()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}