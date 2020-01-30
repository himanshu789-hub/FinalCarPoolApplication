using System;
using System.Collections.Generic;
using System.Text;
namespace CarPoolApplication.Models
{
    public class Offering 
    {
        public string UserId;
        public bool Active;
        public int Discount;
        public Vechicle Cardetails;
        public Journey JourneyDetails;
        public int CurrentLocation;
        public float TotalEarning; 
        
    }
}

