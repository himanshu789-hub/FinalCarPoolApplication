using System;
using System.Collections.Generic;
using System.Text;
namespace CarPoolApplication.Models
{
    public class Offering 
    {
        public string UserId;
        public bool Active;
        public Discount Discount;
        public Vechicle Cardetails = new Vechicle();
        public Journey JourneyDetails =new Journey();
        public Address CurrentLocation;
        public float TotalEarning; 

        public Offering(string userId) { this.UserId = userId; }
        
        public override string ToString()
        {
  return "\nFrom : " + Enum.GetName(typeof(Address), CurrentLocation) + "\nTo : " + Enum.GetName(typeof(Address), JourneyDetails.Destination) +
                "\nFare(Per Km) : "+JourneyDetails.Price+" With A "+((this.Discount==Discount.DISCOUNT_5P)?"5% DISCOUNT":((this.Discount==Discount.DISCOUNT_10P)?"10% DISCOUNT":"20% DISCOUNT"));
        }



    }
}

