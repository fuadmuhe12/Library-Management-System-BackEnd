using Library_Management_System_BackEnd.Helper.Enums;

namespace Library_Management_System_BackEnd.Helper.Query
{
    public class BookQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool Search { get; set; } = false;
        public SearchBy? SearchBy { get; set; } = Enums.SearchBy.Title;
        public string? SearchValue { get; set; }
        public bool IsDecensing { get; set; } = false;
        public SortBy? SortBy { get; set; }
        public Catagoryies? FilterByCategory { get; set; }
        public List<Tags>? FilterByTags { get; set; }
    }
}
