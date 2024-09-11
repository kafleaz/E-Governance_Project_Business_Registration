using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OCR_E_gov.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OCR_E_gov.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    FullName = model.FullName,
                    Email = model.Email
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registration successful! Please login.";
                return RedirectToAction("Register");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                // Set session or authentication cookie for admin
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToAction("CompanyList", "Admin");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                // Set session or authentication cookie for user
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserRole", "User");
                HttpContext.Session.SetString("FullName", user.FullName);
                return RedirectToAction("Create", "Company");
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        private int GetLoggedInUserId()
        {
            return HttpContext.Session.GetInt32("UserId") ?? 0; // Example using session
        }
    }
}
