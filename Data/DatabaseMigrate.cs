using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library_Management_System_BackEnd.Data
{
    public static class DatabaseMigrate
    {
        public static async Task DatabaseMigrateAsync( this WebApplication app){
            using var scope = app.Services.CreateScope();
            LibraryContext libraryContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
            await libraryContext.Database.MigrateAsync();
        }
    }

}