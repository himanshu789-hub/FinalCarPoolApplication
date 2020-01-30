using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using CarPoolApplication.Database;
namespace CarPoolApplication.Services
{
   public interface IUserServices
    {
        User LogIn(Database.Database database, string userId, string password);
        User Create(Database.Database database, string name, string password, Gender gender);
         Booking AddBookingByUserId(Offering offerer, string userId);
        Booking GetBookingByUserId(Database.Database database, string userId);
        Offering GetPreviousCreatedOffer(Database.Database database, string userId);
        User GetUserByUserId(Database.Database database, string userId);
    }
}
