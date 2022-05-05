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
    public class ReviewComentsController : Controller
    {

        private readonly CoolbooksContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReviewComentsController(UserManager<ApplicationUser> userManager, CoolbooksContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Like(int id, int id2)
        {

           

            var reviewComents = new ReviewComents();
            reviewComents.ReviewsID = id;
            reviewComents.React = true;
            reviewComents.ClientId = _userManager.GetUserId(HttpContext.User);

            await _context.ReviewComents.AddAsync(reviewComents);
            _context.SaveChanges();




            return RedirectToAction("Details", "Books", new { id = id2 });

        }

        public async Task<IActionResult> Dislike(int id, int id2)
        {

            var reviewComents = new ReviewComents();
            reviewComents.ReviewsID = id;
            reviewComents.React = false;
            reviewComents.ClientId = _userManager.GetUserId(HttpContext.User);

            await _context.ReviewComents.AddAsync(reviewComents);
            _context.SaveChanges();




            return RedirectToAction("Details", "Books", new { id = id2 });

        }
        public async Task<IActionResult> UpdateDislike(int id, int id2, int id3)
        {

            var reviewComents = new ReviewComents();
            reviewComents.ReviewComentsID = id;
            reviewComents.ReviewsID = id3;
            reviewComents.React = false;
            reviewComents.ClientId = _userManager.GetUserId(HttpContext.User);

            _context.ReviewComents.Update(reviewComents);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Books", new { id = id2 });

        }
        public async Task<IActionResult> UpdateLike(Reviews reviews, int id, int id2, int id3)
        {

            var reviewComents = new ReviewComents();
            reviewComents.ReviewComentsID = id;
            reviewComents.ReviewsID = id3;
            reviewComents.React = true;
            reviewComents.ClientId = _userManager.GetUserId(HttpContext.User);

            _context.ReviewComents.Update(reviewComents);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Books", new { id = id2 });

        }


    }
}
