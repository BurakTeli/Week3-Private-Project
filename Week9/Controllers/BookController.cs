public class BookController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    // List all books
    public IActionResult List()
    {
        var books = _context.Books.Include(b => b.Author).ToList();
        return View(books);
    }

    // Show book details
    public IActionResult Details(int id)
    {
        var book = _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();
        return View(book);
    }

    // Create new book
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Add(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        return View(book);
    }

    // Edit existing book
    public IActionResult Edit(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost]
    public IActionResult Edit(int id, Book book)
    {
        if (id != book.Id) return BadRequest();
        if (ModelState.IsValid)
        {
            _context.Update(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        return View(book);
    }

    // Delete book
    public IActionResult Delete(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();
        return View(book);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var book = _context.Books.FirstOrDefault(b => b.Id == id);
        if (book == null) return NotFound();
        _context.Books.Remove(book);
        _context.SaveChanges();
        return RedirectToAction(nameof(List));
    }
}
