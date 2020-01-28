
using CarPoolApplication.Models;
namespace CarPoolApplication.Services
{
   public class VechicleService:IVechicleService
    {
        
        
        public bool ProvideASeat(Vechicle car)
        {
            if (car.LeftSeats>0 && car.LeftSeats <= car.MaxOfferSeats)
            {
                car.LeftSeats--;
                return true;
            }
                return false;
        }

        public bool DedeuctASeat(Vechicle Car)
        {
            if(Car.LeftSeats>0)
            {
                Car.LeftSeats++;
                return true;
            }
            return false;
        }

      public bool HasSeats(Vechicle Car)
        {
            if (Car.LeftSeats == 0)
                return false;
            return true; 
        }

    }
}
