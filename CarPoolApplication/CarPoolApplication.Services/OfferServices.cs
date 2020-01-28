using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using CarPoolApplication.Database;
namespace CarPoolApplication.Services
{
    public class OfferServices : IOfferServices
    {
        

        public string View(Offering poller)
        {
            return "Get" + poller.Discount + "% offer on this deal.";
        }

        public void Apply(Booking booking, Discount offer)
        {
            booking.JourneyDetails.Price = (((float)(100 - (int)offer)) / 100) * booking.JourneyDetails.Price;
        }

        public List<Offering> GetAllOffersWithinReach(Database.Database pooling, Address start, Address destination)
        {
            List<Offering> offeres;
            if ((destination - start) > 0)
            {
                offeres = pooling.Offerring.FindAll(element =>
               (start>=element.CurrentLocation && destination<=element.JourneyDetails.Destination) && element.Active
                    );
                if (offeres.Count == 0) 
                {
                    offeres = pooling.Offerring.FindAll(element =>
                    (start!= element.JourneyDetails.Destination && start >= element.CurrentLocation) && element.Active);
                }
            }
            else
            {
                offeres = pooling.Offerring.FindAll(element =>
                (
           element.CurrentLocation >= start  && element.JourneyDetails.Destination<=destination) && element.Active
                );

                if (offeres.Count == 0) {
                    offeres = pooling.Offerring.FindAll(element =>
                (
                 start != element.JourneyDetails.Destination &&  element.CurrentLocation >= start) && element.Active
                );
                }
               
            }
            if (offeres.Count == 0)
                return null;
            return offeres;

        }

        public Offering Create(Database.Database pooling, string userId)
        {
            pooling.Offerring.Add(new Offering(userId));
            return pooling.Offerring[pooling.Offerring.Count - 1];
        }


        public Offering GetByUserId(Database.Database dBase, string userId)
        {
            return dBase.Offerring.Find(element => element.Active && element.UserId.Equals(userId));
        }
        
    }
}
