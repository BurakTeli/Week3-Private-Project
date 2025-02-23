public class AuthorController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // List all authors
    public IActionResult List()
    {
        var authors = _context.Authors.ToList();
        return View(authors);
    }

    // Show author details
    public IActionResult Details(int id)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null) return NotFound();
        return View(author);
    }

    // Create new author
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Author author)
    {
        if (ModelState.IsValid)
        {
            _context.Add(author);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        return View(author);
    }

    // Edit existing author
    public IActionResult Edit(int id)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null) return NotFound();
        return View(author);
    }

    [HttpPost]
    public IActionResult Edit(int id, Author author)
    {
        if (id != author.Id) return BadRequest();
        if (ModelState.IsValid)
        {
            _context.Update(author);
            _context.SaveChanges();
            return RedirectToAction(nameof(List));
        }
        return View(author);
    }

    // Delete author
    public IActionResult Delete(int id)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null) return NotFound();
        return View(author);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null) return NotFound();
        _context.Authors.Remove(author);
        _context.SaveChanges();
        return RedirectToAction(nameof(List));
    }
}
