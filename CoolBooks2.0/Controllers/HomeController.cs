using CoolBooks.Areas.Identity;
using CoolBooks.Data;
using CoolBooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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

        public IActionResult Index()
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
                    IsBookOfTheWeek =p.IsBookOfTheWeek,

                    GenreName = (List<string>)p.GenresFromBooks.Select(m => m.Genre.Name),
                    AuthorName = (List<string>)p.AuthorsFromBooks.Select(m => m.Author.FullName),
                    UserName = (List<string>)p.BooksUsers.Select(m => m.Client.UserName),
                })
                .ToList();
            return View(coolbooksContext);
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