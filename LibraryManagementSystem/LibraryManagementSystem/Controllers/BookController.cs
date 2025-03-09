using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    // Controller for managing books
    public class BookController : Controller
    {
        // Static list to store books (temporary storage)
        public static List<BookViewModel> books = new List<BookViewModel>();
        
        // GET: Display the list of books
        public IActionResult List()
        {
            return View(books);
        }

        // GET: Show the form to create a new book
        [HttpGet]
        public IActionResult Create()
        {
            // Populate the authors dropdown
            ViewBag.Authors = AuthorController.authors.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = $"{a.FirstName} {a.LastName}"
            }).ToList();
            return View();
        }

        // POST: Handle the submission of the book creation form
        [HttpPost]
        public IActionResult Create(BookViewModel model)
        {
            var author = AuthorController.authors.FirstOrDefault(a => a.Id == model.AuthorId);

            // Assign a unique ID to the new book
            model.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;

            // Set author name or default to "Unknown Author"
            if (author != null)
            {
                model.AuthorName = $"{author.FirstName} {author.LastName}";
            }
            else
            {
                model.AuthorName = "Unknown Author";
            }

            books.Add(model);

            // Add book to author's list if author exists
            if (author != null)
                author.Books.Add(model);

            return RedirectToAction("List"); // Redirect to the book list
        }

        // GET: Show the form to edit an existing book
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // Populate the authors dropdown
            ViewBag.Authors = AuthorController.authors
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = $"{a.FirstName} {a.LastName}"
                }).ToList();

            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); // Return 404 if the book is not found
            return View(book);
        }

        // POST: Handle the submission of the book edit form
        [HttpPost]
        public IActionResult Edit(BookViewModel updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == updatedBook.Id);
            if (book == null)
                return NotFound(); // Return 404 if the book is not found

            // Remove book from the old author's list
            var oldAuthor = AuthorController.authors.FirstOrDefault(a => a.Id == book.AuthorId);
            if (oldAuthor != null)
            {
                oldAuthor.Books.Remove(book);
            }

            // Update book details
            book.Title = updatedBook.Title;
            book.Genre = updatedBook.Genre;
            book.ISBN = updatedBook.ISBN;
            book.PublishDate = updatedBook.PublishDate;
            book.CopiesAvailable = updatedBook.CopiesAvailable;
            book.AuthorId = updatedBook.AuthorId;

            // Assign book to the new author
            var newAuthor = AuthorController.authors.FirstOrDefault(a => a.Id == updatedBook.AuthorId);
            if (newAuthor != null)
            {
                book.AuthorName = $"{newAuthor.FirstName} {newAuthor.LastName}";
                newAuthor.Books.Add(book);
            }

            return RedirectToAction("List"); // Redirect to the book list
        }

        // GET: Show details of a specific book
        public IActionResult Details(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); // Return 404 if the book is not found
            return View(book);
        }

        // DELETE: Remove a book from the list
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(); // Return 404 if the book is not found

            // Remove book reference from the author's list
            var author = AuthorController.authors.FirstOrDefault(a => a.Id == book.AuthorId);
            if (author != null)
            {
                author.Books.Remove(book);
            }

            // Remove the book from the list
            books.Remove(book);
            
            return RedirectToAction("List"); // Redirect to the book list
        }
    }
}
