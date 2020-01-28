using System.Collections.Generic;
using CarPoolApplication.Models;
using CarPoolApplication.Database;

namespace CarPoolApplication.Services
{
    public class BookingServices : IBookingServices
    {

        public void Book(Database.Database pooling, Offering poller, Address source, Address destination, string UserId)
        {
            Booking booking = new Booking();
            booking.UserId = UserId;
            booking.Active = true;
            booking.BookingStatus = BookingStatus.PENDING;
            booking.OffererId = poller.UserId;
            booking.JourneyDetails.Source = source;
            booking.JourneyDetails.Destination = destination;
            booking.JourneyDetails.Distance = (destination - source);

            if (destination - source > 0)
            {
                booking.JourneyDetails.Distance = (destination - source);
                booking.JourneyDetails.Price = (destination - source) * poller.JourneyDetails.Price;
            }
            else
            {
                booking.JourneyDetails.Price = (destination - source) * poller.JourneyDetails.Price * -1;
                booking.JourneyDetails.Distance = (destination - source) * -1;
            }
            pooling.Bookings.Add(booking);
        }

        public Booking PassBooking(Database.Database dBase, User user, Address source, Address destination)
        {
            Booking booking = new Booking();
            booking.Active = true;
            booking.BookingStatus = BookingStatus.BROADCAST;
            booking.JourneyDetails.Source = source;
            booking.JourneyDetails.Destination = destination;
            booking.UserId = user.Id;
            if (destination - source > 0)
                booking.JourneyDetails.Distance = (destination - source);
            else
                booking.JourneyDetails.Distance = (destination - source) * -1;
            dBase.Bookings.Add(booking);
            return dBase.Bookings[dBase.Bookings.Count - 1];
        }

        public void Accept(Database.Database pooling, Offering poller, Booking pollee)
        {
            pollee.BookingStatus = BookingStatus.ACCEPTED;
        }
        
        public Booking IsAApplicant(Database.Database dBase,string userId)
        {
            return dBase.Bookings.Find(element =>element.UserId.Equals(userId));
        }

        public List<Booking> GetAllRequestedBooking(Database.Database pool, Offering poller)
        {
            List<Booking> pollees = pool.Bookings.FindAll(element =>
            {
                if (element.BookingStatus == BookingStatus.BROADCAST && this.IsUnderRoute(poller.CurrentLocation, element.JourneyDetails.Source, poller.JourneyDetails.Destination, element.JourneyDetails.Destination))
                    return true;
                else
                {
                    if (element.BookingStatus == BookingStatus.PENDING && (element.OffererId.Equals(poller.UserId)))
                        return true;
                    else
                        return false;
                }
            }
            );
            if (pollees.Count == 0)
                return null;
            return pollees;
        }

        public bool IsUnderRoute(Address sourceO, Address sourceA, Address destinationO, Address destinationA)
        {
            if (destinationO - sourceO > 0 && destinationA - sourceA > 0)
            {
                if (sourceA >= sourceO)
                    return true;
            }
            else if (destinationO - sourceO < 0 && destinationA - sourceA < 0)
            {
                if (sourceA <= sourceO)
                    return true;
            }
            return false;
        }
        public void RejectARequest(Booking booking, Vechicle car)
        {
            booking.BookingStatus = BookingStatus.REJECTED;
            new VechicleService().DedeuctASeat(car);
        }

        public void DestroyAllPreviousBooking(Database.Database dBase, Offering offering, Address reachedTill, bool direction)
        {
            List<Booking> bookings;
            if (direction)
            {
                bookings = dBase.Bookings.FindAll(element =>
                  {
                      if (element.BookingStatus == BookingStatus.ACCEPTED)
                      {
                          if (element.OffererId.Equals(offering.UserId) && (element.JourneyDetails.Destination <= reachedTill))
                              return true;
                          else
                              return false;
                      }
                      else
                      {
                         if(element.BookingStatus==BookingStatus.BROADCAST)
                          {
                              element.BookingStatus = BookingStatus.DESTROYED;
                          }
                          return false;
                      }
                  });
            }
            else
            {
           bookings = dBase.Bookings.FindAll(element => 
           {
               if (element.BookingStatus == BookingStatus.ACCEPTED)
               {
                   if (element.OffererId.Equals(offering.UserId) && (element.JourneyDetails.Destination >= reachedTill))
                       return true;
                   else
                       return false;
               }
               else {

                   if (element.BookingStatus == BookingStatus.BROADCAST)
                   {
                       element.BookingStatus = BookingStatus.DESTROYED;
                   }
                   return false;
               }
           });

            }
            if (bookings.Count>0)
            {
                foreach (Booking booking in bookings)
                {
                    offering.TotalEarning += booking.JourneyDetails.Price;
                    new VechicleService().DedeuctASeat(offering.Cardetails);
                    booking.Active = false;
                }
            }
        }


        public void Removed(Booking booking)
        {
            booking.Active = false;
            booking.BookingStatus = BookingStatus.CANCEL;
        }

        public void CancelABookingByOffererId(Database.Database dBase,string offererId,Booking book)
        {
            book.BookingStatus = BookingStatus.CANCEL;
            book.Active = false;
            Offering offer = new OfferServices().GetByUserId(dBase, offererId);
            new VechicleService().DedeuctASeat(offer.Cardetails);
        }


    }
}
