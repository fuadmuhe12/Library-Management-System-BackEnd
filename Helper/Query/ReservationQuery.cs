using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Helper.Enums;

namespace Library_Management_System_BackEnd.Helper.Query
{
    public class ReservationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public ReservationStatus? FilterByReservationStaus { get; set; }

        // book specific
        public bool Search { get; set; } = false;
        public SearchBy? SearchBy { get; set; } = Enums.SearchBy.Title;
        public string? SearchValue { get; set; }
        public bool IsDecensing { get; set; } = false;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SortByReservation? SortBy { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Catagoryies? FilterByCategory { get; set; }
        public List<Tags>? FilterByTags { get; set; }
    }
}
