using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    // Controller for managing authors
    public class AuthorController : Controller
    {
        // Static list to store authors (temporary storage)
        public static List<AuthorViewModel> authors = new List<AuthorViewModel>();

        // GET: Display the list of authors
        public IActionResult List()
        {
            return View(authors);
        }

        // GET: Show the form to create a new author
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Handle the submission of the author creation form
        [HttpPost]
        public IActionResult Create(AuthorViewModel model)
        {
            // Assign a unique ID to the new author
            model.Id = authors.Any() ? authors.Max(a => a.Id) + 1 : 1;
            authors.Add(model);
            return RedirectToAction("List"); // Redirect to the author list
        }

        // GET: Show the form to edit an existing author
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return NotFound(); // Return 404 if the author is not found
            return View(author);
        }

        // POST: Handle the submission of the author edit form
        [HttpPost]
        public IActionResult Edit(AuthorViewModel updateAuthor)
        {
            var author = authors.FirstOrDefault(a => a.Id == updateAuthor.Id);
            if (author == null)
                return NotFound(); // Return 404 if the author is not found
            
            // Update author details
            author.FirstName = updateAuthor.FirstName;
            author.LastName = updateAuthor.LastName;
            author.DateOfBirth = updateAuthor.DateOfBirth;

            // Update the author's name in all associated books
            foreach (var book in author.Books)
            {
                book.AuthorName = $"{author.FirstName} {author.LastName}";
            }
            return RedirectToAction("List"); // Redirect to the author list
        }
        
        // GET: Show details of a specific author
        public IActionResult Details(int id)
        {
            var author = authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return NotFound(); // Return 404 if the author is not found
            return View(author);
        }

        // DELETE: Remove an author from the list
        public IActionResult Delete(int id)
        {
            var author = authors.FirstOrDefault(a => a.Id == id);
            if (author == null) 
                return NotFound(); // Return 404 if the author is not found

            // Update books associated with the deleted author
            foreach (var book in author.Books)
            {
                book.AuthorId = 0;
                book.AuthorName = "Unknown"; 
            }
            
            // Remove the author from the list
            authors.Remove(author);
            return RedirectToAction("List"); // Redirect to the author list
        }
    }
}