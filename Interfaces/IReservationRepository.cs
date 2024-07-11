using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Entities.Models;

namespace Library_Management_System_BackEnd.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetCurrentReservation(int bookId);
        Task UpdateReservationStatus(int id, ReservationStatus newStatus);
        Task CreateReservation(int bookId, string userId);
        Task<bool> IsUserReservedBook(string userId, int bookId);
        Task<Reservation?> GetReservationById(int id);

        Task<List<Reservation>> GetReservations(int bookId);

    }
}
