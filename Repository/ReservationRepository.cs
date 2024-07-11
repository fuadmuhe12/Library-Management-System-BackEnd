using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Entities.Models;
using Library_Management_System_BackEnd.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_BackEnd.Repository
{
    public class ReservationRepository(LibraryContext context) : IReservationRepository
    {
        private readonly LibraryContext _context = context;

        public async Task<Reservation?> GetCurrentReservation()
        {
            var reservations = _context.Reservations.OrderBy(reser => reser.ReservationDate);
            return await reservations
                .Where(reser => reser.Status == ReservationStatus.Pending)
                .FirstAsync();
        }

        public async Task UpdateReservationStatus(int id, ReservationStatus newStatus)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(reser =>
                reser.ReservationId == id
            );
            if (reservation != null)
            {
                reservation.Status = newStatus;
            }
        }
    }
}
