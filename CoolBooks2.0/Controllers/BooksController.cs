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

namespace CoolBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly CoolbooksContext _context;

        public BooksController(CoolbooksContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {


            var coolbooksContext = _context.Books
                //.Include(b => b.AuthorsFromBooks)
                //.ThenInclude(b => b.Author)
                //.Include(a => a.GenresFromBooks)
                //.ThenInclude(a => a.Genre)
                .Select( p => new BooksViewModel
                {
                    BooksID = p.BooksID,
                    Title = p.Title,
                    Description = p.Description,
                    ISBN = p.ISBN,
                    ImagePath = p.ImagePath,
                    GenreName= (List<string>)p.GenresFromBooks.Select(m => m.Genre.Name),
                    AuthorName = (List<string>)p.AuthorsFromBooks.Select(m => m.Author.FullName),

                })
                
                
                .ToList();

            //var coolbooksAuthors = _context.Books
                //.Include(b => b.AuthorsFromBooks)
                //.ThenInclude(b => b.Author)
                //.Include(a => a.GenresFromBooks)
                //.ThenInclude(a => a.Genre)
                //.SelectMany(a => a.AuthorsFromBooks, (c, i) => new BooksViewModel
                //{
                //    BooksID= c.BooksID,
                //    AuthorName = i.Author.FullName,

                //})


                //.ToList();
                //var ResultantList = coolbooksContext.Where(s => coolbooksAuthors.Any(l => (l.BooksID == s.BooksID))).Concat(coolbooksAuthors).ToList();

            //var result = coolbooksContext.ForEach(i => coolbooksAuthors.Add(i));

            //var view = new BooksViewModel();

            //IEnumerable<BooksViewModel> books;

            //books = (IEnumerable<BooksViewModel>)coolbooksContext;

            //foreach (var Item in coolbooksContext)
            //{

            //    view.BooksID = Item.BooksID;
            //    view.Description = Item.Description;
            //    view.ISBN = Item.ISBN;
            //    view.ImagePath = Item.ImagePath;

            //        foreach (var item in Item.GenresFromBooks)
            //        {
            //        //view.GenreName = item.Genre.Name;
            //        view.GenreName.Add(item.Genre.Name);
            //        }

            //        foreach (var item in Item.AuthorsFromBooks)
            //        {
            //        //view.AuthorName = item.Author.FullName;
            //        view.AuthorName.Add(item.Author.FullName);
            //        }

            //}

            return View(coolbooksContext);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }




            //if (books == null)
            //{
            //    return NotFound();
            //}

            return View();
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "FullName");
            ViewData["GenerID"] = new SelectList(_context.Genres, "GenreID", "Name");
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

        var books = await _context.Books
             .Include(s => s.AuthorsFromBooks).ThenInclude(s => s.Author)
                 .Where(s => s.BooksID == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();
            
            

        if (books == null)
        {
            return NotFound();
        }
        return View(books);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BooksID,UserID,AuthorID,Title,Description,ISBN,ImagePath,IsDeleted,Created,GenerID")] Books books)
        {
            if (id != books.BooksID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(books);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.BooksID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "AuthorID", books.AuthorsFromBooks);
            ViewData["GenerID"] = new SelectList(_context.Genres, "GenerID", "GenerID", books.GenresFromBooks);
            return View(books);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _context.Books.FindAsync(id);
            _context.Books.Remove(books);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BooksExists(int id)
        {
            return _context.Books.Any(e => e.BooksID == id);
        }
    }
}
