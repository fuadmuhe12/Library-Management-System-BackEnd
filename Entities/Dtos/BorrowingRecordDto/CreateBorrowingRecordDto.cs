using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Entities.Dtos.BorrowingRecordDto
{
    /*  public int BorrowingRecordId { get; set; }
        required public string UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(14);
        public DateTime? ReturnDate { get; set; }
        public decimal? FineAmount { get; set; } */
    public class CreateBorrowingRecordDto
    {
        required public string UserId { get; set; }
        public int BookId { get; set; }
    }
}
