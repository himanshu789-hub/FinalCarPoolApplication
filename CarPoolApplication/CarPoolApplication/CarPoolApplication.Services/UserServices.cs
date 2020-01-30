using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Database;
using CarPoolApplication.Models;
namespace CarPoolApplication.Services
{
   public class UserServices:IUserServices
    {

        public User LogIn(Database.Database database, string userId, string password)
        {
            User user = database.Users.Find(element => element.Id.Equals(userId) && element.IsPasswordValid(password));
            return user;
        }

        public User Create(Database.Database database,string name,string password,Gender gender)
        {
            User user = new User(password,name);
            database.Users.Add(user);
            return database.Users[database.Users.Count - 1];
        }

       
        public Booking AddBookingByUserId(Offering offerer, string userId)
        {
            Booking booking = new Booking();
            booking.OffererId = offerer.UserId;
            booking.UserId = userId;
            return booking;
        }


        public Booking GetBookingByUserId(Database.Database pool,string userId)
        {
            Booking pollee = pool.Bookings.Find(element => element.UserId.Equals(userId) && element.Active);
            return pollee;
        }

        public Offering GetPreviousCreatedOffer(Database.Database pool, string userId)
        {
            return pool.Offerring.Find(element => element.UserId.Equals(userId) && element.Active);
        }


        public User GetUserByUserId(Database.Database pool,string userId)
        {
            return pool.Users.Find(user => user.Id.Equals(userId));
        }

      
    }
}
