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


namespace CoolBooks.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly CoolbooksContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReviewsController(UserManager<ApplicationUser> userManager, CoolbooksContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reviews.ToListAsync());
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ReviewsID == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // GET: Reviews/Create
        public IActionResult Create(int id, int id2)
        {
            ViewData["AllAuthors"] = _context.Authors.Where(x => x.AuthorID == id).ToList();
            ViewData["AllBooks"] = _context.Books.Where(x => x.BooksID == id2).ToList();
            if (id2 == null)
            {
                ViewData["AllBooks"] = null;
            }

            ViewData["BookID"] = new SelectList(_context.Books, "BooksID", "Title");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BooksViewModel booksView)
        {


            var review = new Reviews();
            review.BookID = booksView.BooksID;
            review.Title = booksView.Title;
            review.Text = booksView.ReviewText;
            review.Rating = booksView.ReviewRating;
            review.ClientId = _userManager.GetUserId(HttpContext.User);
            review.UserName = _userManager.GetUserName(HttpContext.User);
            review.Created = DateTime.Now.Date;


            await _context.Reviews.AddAsync(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "Books", new { id = booksView.BooksID });

        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }
            return View(reviews);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReviewsID,BookID,UserID,Title,Text,Rating,IdDeleted,Created")] Reviews reviews)
        {
            if (id != reviews.ReviewsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewsExists(reviews.ReviewsID))
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
            return View(reviews);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviews = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ReviewsID == id);
            if (reviews == null)
            {
                return NotFound();
            }

            return View(reviews);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reviews = await _context.Reviews.FindAsync(id);
            
            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewsExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewsID == id);
        }

        public async Task<IActionResult> NotFlag(int id, int id2)
        {
            var review = _context.Reviews.Where(r=>r.ReviewsID == id).FirstOrDefault();
            review.Flag = true;

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Books", new { id = id2 });

        }

        public async Task<IActionResult> IsFlag(int id, int id2)
        {
            var review = _context.Reviews.Where(r => r.ReviewsID == id).FirstOrDefault();
            review.Flag = false;

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Books", new { id = id2 });

        }
    }
}
