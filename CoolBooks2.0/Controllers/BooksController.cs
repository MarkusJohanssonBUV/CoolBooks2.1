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
            var coolbooksContext = _context.Books.Include(b => b.BooksAuthors).Include(b => b.BooksGenres);
            ViewData["Author"] = _context.Authors;
            return View(await coolbooksContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _context.Books
                .Include(b => b.BooksAuthors)
                .Include(b => b.BooksGenres)
                .FirstOrDefaultAsync(m => m.BooksID == id);
            if (books == null)
            {
                return NotFound();
            }

            return View(books);
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
        public async Task<IActionResult> Create([Bind("BooksID,UserID,AuthorID,Title,Description,ISBN,ImagePath,IsDeleted,Created,GenerID")] Books books)
        {
            if (ModelState.IsValid)
            {
                _context.Add(books);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "AuthorID", books.BooksAuthors);
            //ViewData["GenerID"] = new SelectList(_context.Genres, "GenerID", "GenerID", books.BooksGenres);
            return View(books);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           if (id == null)
        {
            return NotFound();
        }

        var books = await _context.Books
             .Include(s => s.BooksAuthors).ThenInclude(s => s.Author)
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
            ViewData["AuthorID"] = new SelectList(_context.Authors, "AuthorID", "AuthorID", books.BooksAuthors);
            ViewData["GenerID"] = new SelectList(_context.Genres, "GenerID", "GenerID", books.BooksGenres);
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
                .Include(b => b.BooksAuthors)
                .Include(b => b.BooksGenres)
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
