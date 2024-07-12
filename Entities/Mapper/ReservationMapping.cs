using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Dtos.ReservationDto;
using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Entities.Mapper
{
    public static class ReservationMapping
    {
        public static ReservationViewDto MapToViewReservation(this Reservation reservation)
        {
            return new ReservationViewDto
            {
                ReservationId = reservation.ReservationId,
                Book = reservation.Book!.ToViewFromBook(),
                User = reservation.User!.MapToUserMinimalDto(),
                ReservationDate = reservation.ReservationDate,
                ReservationStatus = reservation.Status.ToString()
            };
        }
    }
}
