using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calischool.Data;
using Calischool.Models;
using Microsoft.AspNetCore.Authorization;

namespace Calischool.Controllers
{

    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Dashboard()
        {
            var applicationUser = new List<ApplicationUser>();
            var users = _context.StudentRegisters.Where(a => a.RegNumber != null && a.Email != null/* && a.Deleted == false*/).ToList();
            if (users.Count > 0)
            {
                return View(users);
            }
            return View(applicationUser);
        }

    }


}
