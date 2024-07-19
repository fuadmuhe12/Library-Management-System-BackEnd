using Library_Management_System_BackEnd.Entities.Dtos.AuthDto;
using Library_Management_System_BackEnd.Entities.Dtos.BookDto;

namespace Library_Management_System_BackEnd.Entities.Dtos.BorrowingRecordDto
{
    public class ViewBorrowingRecord
    {
        public int BorrowingRecordId { get; set; }
        public UserMinimalViewDto? User { get; set; }
        public ViewBookDto? Book { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; } 
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;
    }
}
