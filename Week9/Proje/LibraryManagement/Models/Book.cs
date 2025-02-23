using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagement.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        public string Genre { get; set; }

        public DateTime PublishDate { get; set; }

        [Required]
        public string ISBN { get; set; }

        public int CopiesAvailable { get; set; }

        public virtual Author Author { get; set; }
    }
}
