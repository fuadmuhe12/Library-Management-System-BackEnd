namespace Library_Management_System_BackEnd;

public class AuthorQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? AuthorNameSearch { get; set; }
    public bool IsDecensing { get; set; } = true;
}
