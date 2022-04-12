//#nullable disable
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using CoolBooks.Data;
//using CoolBooks.Models;

//namespace CoolBooks.Controllers
//{
//    public class UserInfoesController : Controller
//    {
//        private readonly CoolbooksContext _context;

//        public UserInfoesController(CoolbooksContext context)
//        {
//            _context = context;
//        }

//        // GET: UserInfoes
//        public async Task<IActionResult> Index()
//        {
//            var coolbooksContext = _context.UserInfo.Include(u => u.UserID).Include(u => u.Reviews);
//            return View(await coolbooksContext.ToListAsync());
//        }

//        // GET: UserInfoes/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var userInfo = await _context.UserInfo
//                .Include(u => u.BooksUsers)
//                .Include(u => u.Reviews)
//                .FirstOrDefaultAsync(m => m.UserInfoID == id);
//            if (userInfo == null)
//            {
//                return NotFound();
//            }

//            return View(userInfo);
//        }

//        // GET: UserInfoes/Create
//        public IActionResult Create()
//        {
//            ViewData["BooksID"] = new SelectList(_context.Books, "BooksID", "BooksID");
//            ViewData["ReviewsID"] = new SelectList(_context.Reviews, "ReviewsID", "ReviewsID");
//            return View();
//        }

//        // POST: UserInfoes/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("UserInfoID,UserID,ReviewsID,BooksID,FirstName,LastName,PhoneNumber,Adress,Created")] UserInfo userInfo)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(userInfo);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["BooksID"] = new SelectList(_context.Books, "BooksID", "BooksID", userInfo.BooksUsers);
//            ViewData["ReviewsID"] = new SelectList(_context.Reviews, "ReviewsID", "ReviewsID", userInfo.Reviews);
//            return View(userInfo);
//        }

//        // GET: UserInfoes/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var userInfo = await _context.UserInfo.FindAsync(id);
//            if (userInfo == null)
//            {
//                return NotFound();
//            }
//            ViewData["BooksID"] = new SelectList(_context.Books, "BooksID", "BooksID", userInfo.BooksUsers);
//            ViewData["ReviewsID"] = new SelectList(_context.Reviews, "ReviewsID", "ReviewsID", userInfo.Reviews);
//            return View(userInfo);
//        }

//        // POST: UserInfoes/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("UserInfoID,UserID,ReviewsID,BooksID,FirstName,LastName,PhoneNumber,Adress,Created")] UserInfo userInfo)
//        {
//            if (id != userInfo.UserInfoID)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(userInfo);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UserInfoExists(userInfo.UserInfoID))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["BooksID"] = new SelectList(_context.Books, "BooksID", "BooksID", userInfo.BooksUsers);
//            ViewData["ReviewsID"] = new SelectList(_context.Reviews, "ReviewsID", "ReviewsID", userInfo.Reviews);
//            return View(userInfo);
//        }

//        // GET: UserInfoes/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var userInfo = await _context.UserInfo
//                .Include(u => u.BooksUsers)
//                .Include(u => u.Reviews)
//                .FirstOrDefaultAsync(m => m.UserInfoID == id);
//            if (userInfo == null)
//            {
//                return NotFound();
//            }

//            return View(userInfo);
//        }

//        // POST: UserInfoes/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var userInfo = await _context.UserInfo.FindAsync(id);
//            _context.UserInfo.Remove(userInfo);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool UserInfoExists(int id)
//        {
//            return _context.UserInfo.Any(e => e.UserInfoID == id);
//        }
//    }
//}
