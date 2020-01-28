using System.Collections.Generic;
using CarPoolApplication.Models;
using CarPoolApplication.Database;

namespace CarPoolApplication.Services
{
   public interface IBookingServices
    {
         void Book(Database.Database database, Offering offerer, Address source, Address destination, string UserId);
         void Accept(Database.Database database, Offering offerer, Booking booking);
         List<Booking> GetAllRequestedBooking(Database.Database database, Offering offerer);
         void DestroyAllPreviousBooking(Database.Database database, Offering offerer, Address reachedTill,bool direction);
         void Removed(Booking booking);

    }
}
