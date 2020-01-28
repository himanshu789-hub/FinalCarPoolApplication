using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
namespace CarPoolApplication.Database
{
   public class Database
    {
        public List<User> Users = new List<User>();
        public List<Offering> Offerring =  new List<Offering>();
        public List<Booking> Bookings =  new List<Booking>();
    }
}
