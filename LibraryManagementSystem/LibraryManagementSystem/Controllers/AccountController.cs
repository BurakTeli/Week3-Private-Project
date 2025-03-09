using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    // Controller for user authentication and authorization
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        // Constructor to initialize the web host environment
        public AccountController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // GET: Display the Login view
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Handle the Login form submission
        [HttpPost]
        public IActionResult Login(LoginFormViewModel model)
        {
            // Validate the input model
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Simple authentication check (hardcoded credentials for demo purposes)
            if (model.Email == "burak@gmail.com" && model.Password == "1234")
            {
                return RedirectToAction("Index", "Home"); // Redirect to home page on successful login
            }
            
            // Show error message for invalid credentials
            ModelState.AddModelError(string.Empty, "Invalid email or password");
            return View(model);
        }
    }
}
