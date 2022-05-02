﻿using CoolBooks.Data;
using CoolBooks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoolBooks.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly CoolbooksContext _context;

        public AdminController(RoleManager<IdentityRole> roleManager, CoolbooksContext context)
        {
            this.roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateRoles()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoles(RolesAdmin roles)
        {
            var roleExist = await roleManager.RoleExistsAsync(roles.RoleName);
            if (!roleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roles.RoleName));
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageQuotes()
        {
            var quotes = from q in _context.Quotes select q;
            //var quotes = _context.Quotes;
            //Ska ta in alla Quotes och avmarkera/markera som moderated
            var qouteview = new QuoteViewModel
            {
                Quotes = quotes.ToList()
            };
            return View(qouteview);

        }

        [HttpPost]
        public IActionResult ManageQuotes(QuoteViewModel test)
        {
            
            foreach (var quote in test.Quotes)
            {

                _context.Quotes.Update(quote);
            }
           
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Quotes");
        }

    }

    

}
