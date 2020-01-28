using System;
using System.Collections.Generic;
using System.Text;
namespace CarPoolApplication.Models
{
   public class Booking
    {
        public string Id;
        public string OffererId;
        public Journey JourneyDetails = new Journey();
        public string UserId;
        public BookingStatus BookingStatus;
        public bool Active;

        public Booking()
        {
            Id = "BOOK"+DateTime.UtcNow; 
        }        

        public string ToString()
        {
            return "By : "+UserId+"\n"+JourneyDetails.ToString()+" \nIn"+Enum.GetName(typeof(BookingStatus),BookingStatus);
        }
    }
}
