using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Enums;

namespace Library_Management_System_BackEnd.Helper.Query
{
    /*  public int ReservationId { get; set; }
        required public string UserId { get; set; }
        public User? User { get; set; }
        public int BookId { get; set; }
        public Book? Book { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
        public bool NotificationSent { get; set; } = false;
        public DateTime? NotificationTime { get; set; } */
    public class ReservationQuery
    {
        public int PageNumber { get; set; }  =1;
        public int PageSize { get; set; } = 10;
        public ReservationStatus? FilterByReservationStaus { get; set; }

        // book specific
        public bool Search { get; set; } = false;
        public SearchBy? SearchBy { get; set; } = Enums.SearchBy.Title;
        public string? SearchValue { get; set; }
        public bool IsDecensing { get; set; } = false;
        public SortByReservation? SortBy { get; set; }
        public Catagoryies? FilterByCategory { get; set; }
        public List<Tags>? FilterByTags { get; set; }



    }
}
