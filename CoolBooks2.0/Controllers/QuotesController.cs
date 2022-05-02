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
    public class QuotesController : Controller
    {
        private readonly CoolbooksContext _context;

        public QuotesController(CoolbooksContext context)
        {
            _context = context;
        }

        // GET: Quotes
        public IActionResult Index()
        {
            return View( _context.Quotes.ToList());
        }

        // GET: Quotes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes
                .FirstOrDefaultAsync(m => m.QuoteId == id);
            if (quotes == null)
            {
                return NotFound();
            }

            return View(quotes);
        }

        // GET: Quotes/Create
        public IActionResult Create()
        {
            
            ViewData["AllBooks"] = _context.Books.ToList();
            ViewData["AllAuthors"] = _context.Authors.ToList();

            //ViewData["AllBooks"] = new SelectList(_context.Books, "BooksD", "Title"); //genre.ToList(); 
            //ViewData["AllAuthors"] = new SelectList(_context.Authors, "AuthorID", "FullName"); //genre.ToList(); 
            return View();
        }

        // POST: Quotes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BooksViewModel quotes)
        {
            var quote = new Quotes()
            {
                Quote = quotes.Quote.FirstOrDefault(),
                BookID = quotes.BooksID,
                AuthorID = quotes.AutorsId.FirstOrDefault(),
                
            };
            await _context.Quotes.AddAsync(quote);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Quotes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes.FindAsync(id);
            if (quotes == null)
            {
                return NotFound();
            }
            return View(quotes);
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuoteId,Quote,IsQuoteModerated,BookID,AuthorID")] Quotes quotes)
        {
            if (id != quotes.QuoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quotes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotesExists(quotes.QuoteId))
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
            return View(quotes);
        }

        // GET: Quotes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotes = await _context.Quotes
                .FirstOrDefaultAsync(m => m.QuoteId == id);
            if (quotes == null)
            {
                return NotFound();
            }

            return View(quotes);
        }

        // POST: Quotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quotes = await _context.Quotes.FindAsync(id);
            _context.Quotes.Remove(quotes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuotesExists(int id)
        {
            return _context.Quotes.Any(e => e.QuoteId == id);
        }
    }
}
