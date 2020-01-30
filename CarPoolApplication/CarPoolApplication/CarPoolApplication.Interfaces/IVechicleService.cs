
using CarPoolApplication.Models;
namespace CarPoolApplication.Services
{
   public interface IVechicleService
    { 
       bool ProvideASeat(Vechicle car);
       bool DedeuctASeat(Vechicle Car);
    }
}
