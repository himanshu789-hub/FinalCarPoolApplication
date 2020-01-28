using System;
using System.Collections.Generic;
using System.Text;
using CarPoolApplication.Models;
using CarPoolApplication.Database;
namespace CarPoolApplication.Services
{
  public interface IOfferServices
    {
         Offering Create(Database.Database dBase,string offererId);
         string View(Offering offerer);
         void Apply(Booking booking, Discount offer);
    }
}
