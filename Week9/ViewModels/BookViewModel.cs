public class BookViewModel
{
    public int Id { get; set; } 
    public string Title { get; set; } 
    public string AuthorName { get; set; } // Full name of the author
    public string Genre { get; set; } 
    public DateTime PublishDate { get; set; } 
    public string ISBN { get; set; } 
    public int CopiesAvailable { get; set; } 
}
