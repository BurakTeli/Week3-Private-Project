public class Book
{
    public int Id { get; set; } // Unique identifier
    public string Title { get; set; } // Book title
    public int AuthorId { get; set; } // Foreign key to Author
    public Author Author { get; set; } // Navigation property to Author
    public string Genre { get; set; } // Genre of the book
    public DateTime PublishDate { get; set; } // Date of publication
    public string ISBN { get; set; } // ISBN number
    public int CopiesAvailable { get; set; } // Number of copies available
}
