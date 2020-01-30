using System;
using System.Collections.Generic;
using System.Text;
namespace CarPoolApplication.Models
{
   public class Booking
    {
        public string Id;
        public string OffererId;
        public Journey JourneyDetails;
        public string UserId;
        public int BookingStatus;
        public bool Active;
        
    }
}
