#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoolBooks.Data;
using CoolBooks.Models;
using Microsoft.AspNetCore.Identity;
using CoolBooks.Areas.Identity;
using Microsoft.AspNetCore.Authorization;



namespace CoolBooks.Controllers
{
    
    public class BooksController : Controller
    {
        private readonly CoolbooksContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        

        public BooksController(CoolbooksContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            this.roleManager = roleManager;
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
                    IsDeleted = p.IsDeleted,
                    GenresId = (List<int>)p.GenresFromBooks.Select(m => m.Genre.GenreID),
                    GenreName = (List<string>)p.GenresFromBooks.Select(m => m.Genre.Name),
                    AutorsId = (List<int>)p.AuthorsFromBooks.Select(m => m.Author.AuthorID),
                    AuthorName = (List<string>)p.AuthorsFromBooks.Select(m => m.Author.FullName),
                    UserName = (List<string>)p.BooksUsers.Select(m => m.Client.UserName),
                    Reviews = p.Reviews.ToList(),
                    Quotes = p.Quotes.ToList()
                    
                })
                .ToList();

            return coolbooksContext;
        }
        public async Task<IActionResult> Index(string Search, string sortOrder, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "Author" ? "Author_desc" : "Author";
            ViewBag.GenreSortParm = sortOrder == "Genre" ? "Genre_desc" : "Genre";

            if (Search == null)
            {
                Search = currentFilter;
            }
            

            ViewBag.CurrentFilter = Search;

            var coolbooksContext = GetAllBooks();

            if (!string.IsNullOrEmpty(Search))
            {
                coolbooksContext = GetAllBooks().Where(p => p.Title.ToLower().Contains(Search.ToLower()) ||
                 p.GenreName.Where(x => x.ToLower().Contains(Search.ToLower())).Any() ||
                 p.AuthorName.Where(x => x.ToLower().Contains(Search.ToLower())).Any() ||
                 p.UserName.Where(x => x.ToLower().Contains(Search.ToLower())).Any()
                 ).ToList();
            }

            switch (sortOrder)
            {
                case "Title_desc":
                    coolbooksContext = coolbooksContext.OrderByDescending(s => s.Title).ToList();
                    break;
                case "Author":
                    coolbooksContext = coolbooksContext.OrderBy(s => s.AuthorName[0]).ToList(); 
                    break;
                case "Author_desc":
                    coolbooksContext = coolbooksContext.OrderByDescending(s => s.AuthorName[0]).ToList(); 
                    break;
                case "Genre":
                    coolbooksContext = coolbooksContext.OrderBy(s => s.GenreName[0]).ToList(); 
                    break;
                case "Genre_desc":
                    coolbooksContext = coolbooksContext.OrderByDescending(s => s.GenreName[0]).ToList();
                    break;
                default:
                    coolbooksContext = coolbooksContext.OrderByDescending(s => s.UserName[0] == _userManager.GetUserName(HttpContext.User)).ToList(); 
                    break;
            }


            
            return View(coolbooksContext); 
           

        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            
            var coolbooksContext =GetAllBooks().Where(p => p.BooksID == id).FirstOrDefault();
            ViewData["AllReviewComents"] = _context.ReviewComents.Select(x => new { x.React, x.ClientId, x.ReviewsID, x.ReviewComentsID}).ToList();
            return View(coolbooksContext);

        }

        // GET: Books/Create
        [Authorize(Roles = ("Admin, User"))]
        public IActionResult Create()
        {
            ViewData["AllGenres"] = new SelectList(_context.Genres, "GenreID", "Name"); //genre.ToList(); 
            ViewData["AllAuthors"] = new SelectList(_context.Authors, "AuthorID", "FullName"); //genre.ToList(); 
            
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Create(BooksViewModel booksView)
        {
            var book = new Books();
            book.Title = booksView.Title;
            book.Description = booksView.Description;
            book.ISBN = booksView.ISBN;
            book.ImagePath = booksView.ImagePath;
            book.Created = DateTime.Now.Date;
            

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();

            foreach (var AutorId in booksView.AutorsId)
            {

                var bookArthor = new BooksAuthors();
                bookArthor.AuthorID = AutorId;
                bookArthor.BooksID = book.BooksID;
                await _context.BooksAuthors.AddAsync(bookArthor);
                
            }
            await _context.SaveChangesAsync();

         
                var bookUser = new BooksUsers();
                bookUser.ClientId = _userManager.GetUserId(HttpContext.User);
                bookUser.BooksID = book.BooksID;
                await _context.BooksUsers.AddAsync(bookUser);
                await _context.SaveChangesAsync();

            foreach (var GenreId in booksView.GenresId)
            {

                var bookGenre = new BooksGenres();
                bookGenre.GenreID = GenreId;
                bookGenre.BooksID = book.BooksID;
                await _context.BooksGenres.AddAsync(bookGenre);
                
            }
            await _context.SaveChangesAsync();
            

            return RedirectToAction(nameof(Index));
            
        }

        private List<int> GetBookIDFromUser()
        {
            var bookId = _context.BooksUsers.Where(c => c.ClientId == _userManager.GetUserId(HttpContext.User)).Select(u => u.BooksID).ToList();

            return bookId;
        }

       
        [Authorize]
      
        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
               if (id == null)
                {
                    return NotFound();
                }

          
                if (!GetBookIDFromUser().Contains(id) && User.IsInRole("User"))
                {
                    return RedirectToAction(nameof(Index));
                }
               
                  
            var coolbooksContextUser = GetAllBooks().Where(p => p.BooksID == id).FirstOrDefault();

                    ViewData["AllGenres"] = _context.Genres.ToList(); 
                    ViewData["AllAuthors"] = _context.Authors.ToList(); 


                    return View(coolbooksContextUser);
                            
        }

      

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BooksViewModel booksView)
        {

            var book = new Books()
            {
                BooksID = booksView.BooksID,
                Title = booksView.Title,
                Description = booksView.Description,
                ISBN = booksView.ISBN,
                ImagePath = booksView.ImagePath
            };

            var booksAuthors = new List<BooksAuthors>();

            var baList = await _context.BooksAuthors.Where(td => td.BooksID == booksView.BooksID).ToListAsync();
            _context.RemoveRange(baList);

           
            foreach (var AutorId in booksView.AutorsId)
            {

                var bookArthor = new BooksAuthors()
                {
                    AuthorID = AutorId,
                    BooksID = booksView.BooksID
                };

                booksAuthors.Add(bookArthor);
            }

            var booksGenres = new List<BooksGenres>();

            var bgList = await _context.BooksGenres.Where(td => td.BooksID == booksView.BooksID).ToListAsync();
            _context.RemoveRange(bgList);

            foreach (var GenreId in booksView.GenresId)
            {

                var bookGenre = new BooksGenres()
                {
                    GenreID = GenreId,
                    BooksID = booksView.BooksID
                };
                booksGenres.Add(bookGenre);

            }

            _context.AddRange(booksGenres);
            _context.AddRange(booksAuthors);
            _context.Books.Update(book);

            await _context.SaveChangesAsync();
            
            

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(b => b.AuthorsFromBooks)
                .Include(b => b.GenresFromBooks)
                .FirstOrDefaultAsync(m => m.BooksID == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Books/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(BooksViewModel booksView)
        {
            var book = _context.Books.FirstOrDefault(u => u.BooksID == booksView.BooksID);
            book.IsDeleted = true;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Revoke(BooksViewModel booksView)
        {
            var book = _context.Books.FirstOrDefault(u => u.BooksID == booksView.BooksID);
            book.IsDeleted = false;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.BooksID == id);
        }
    }
}
