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
        public BooksController(CoolbooksContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Books

        public async Task<IActionResult> Index(string Search)
        {
            //Search = Search.ToLower();

            var coolbooksContext = _context.Books
                .Select( p => new BooksViewModel
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
                })
                .ToList();

            if (!string.IsNullOrEmpty(Search))
            {
                coolbooksContext = coolbooksContext.Where(p => p.Title.ToLower().Contains(Search.ToLower()) ||
                 p.GenreName.Where(x=> x.ToLower().Contains(Search.ToLower())).Any()  ||
                 p.AuthorName.Where(x => x.ToLower().Contains(Search.ToLower())).Any() ||
                 p.UserName.Where(x => x.ToLower().Contains(Search.ToLower())).Any()
                 ).ToList();
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


            var   coolbooksContext = _context.Books
                .Where(p => p.BooksID == id)
                .Select(p => new BooksViewModel
                {
                    BooksID = p.BooksID,
                    Title = p.Title,
                    Description = p.Description,
                    ISBN = p.ISBN,
                    ImagePath = p.ImagePath,
                    Created = p.Created,
                    GenreName = (List<string>)p.GenresFromBooks.Select(m => m.Genre.Name),
                    AuthorName = (List<string>)p.AuthorsFromBooks.Select(m => m.Author.FullName),
                    Reviews = p.Reviews.ToList()
                }).AsEnumerable();

            //coolbooksContext.ToList();

            return View(coolbooksContext);


            //if (books == null)
            //{
            //    return NotFound();
            //}


        }

        // GET: Books/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AllGenres"] = new SelectList(_context.Genres, "GenreID", "Name"); //genre.ToList(); 
            ViewData["AllAuthors"] = new SelectList(_context.Authors, "AuthorID", "FullName"); //genre.ToList(); 
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BooksViewModel booksView)
        {
            var book = new Books();
            book.Title = booksView.Title;
            book.Description = booksView.Description;
            book.ISBN = booksView.ISBN;
            book.ImagePath = booksView.ImagePath;
            book.Created = DateTime.Now;
            

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
            //if (ModelState.IsValid)
            //{
            //    _context.Add(book);
            //    await _context.SaveChangesAsync();

            //}
            //ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "AuthorID", books.BooksAuthors);
            //ViewData["GenerID"] = new SelectList(_context.Genres, "GenerID", "GenerID", books.BooksGenres);

            return RedirectToAction(nameof(Index));
            //return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           if (id == null)
        {
            return NotFound();
        }



            var coolbooksContext = _context.Books
                  .Where(p => p.BooksID == id)
                  .Select(p => new BooksViewModel
                  {
                      BooksID = p.BooksID,
                      Title = p.Title,
                      Description = p.Description,
                      ISBN = p.ISBN,
                      ImagePath = p.ImagePath,
                      Created = p.Created,
                      IsDeleted = p.IsDeleted,
                      GenreNameSelect = new SelectList(p.GenresFromBooks.Select(m => m.Genre), "GenreID", "Name"),
                      AuthorNameSelect = new SelectList(p.AuthorsFromBooks.Select(m => m.Author), "AuthorID", "FullName"), //Gör en SelectList av GenreName i ViewModel om det ska fungera.
                      UserName = (List<string>)p.BooksUsers.Select(m => m.Client.FullName),
                      GenreName = (List<string>)p.GenresFromBooks.Select(m => m.Genre.Name),
                      AuthorName = (List<string>)p.AuthorsFromBooks.Select(m => m.Author.FullName),
                      GenresId = (List<int>)p.GenresFromBooks.Select(m => m.Genre.GenreID),
                      AutorsId = (List<int>)p.AuthorsFromBooks.Select(m => m.Author.AuthorID),
                      
                  }).FirstOrDefault();

            ViewData["AllGenres"] = _context.Genres.ToList(); //Försöker få att det förvalda värdet ska vara värdet som tillhör boken
            ViewData["AllAuthors"] = _context.Authors.ToList(); //genre.ToList(); 


            return View(coolbooksContext);
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
