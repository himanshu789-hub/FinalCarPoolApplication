using System;
using System.Collections.Generic;
using CarPoolApplication.Models;
using CarPoolApplication.Services;
using CarPoolApplication.Utilities;
namespace CarPoolApplication
{
    class Routine
    {
        internal void SignUp(Database.Database dBase)
    
            {
                    Console.WriteLine("Enter Your Name : ");
                    string name = new Exceptions().InputAtLeastOneCharacter();
                    Console.WriteLine("Enter Your Password : ");
                    string password = new Exceptions().InputAtLeastOneCharacter();
                    ConsoleKeyInfo key;
                while (true)
                {
                    Console.WriteLine("Gender[MALE(0),FEMALE(1)] : ");
                    key = Console.ReadKey(true);
                    if (key.KeyChar >= '0' && key.KeyChar < '2')
                        break;
                    else
                    {
                        Console.WriteLine("Please, Enter A Valid Value .");
                        continue;
                    }
                }
                User user = new UserServices().Create(dBase, name, password, ((int)key.KeyChar) == 0 ? Gender.MALE : Gender.FEMALE);
                Console.WriteLine("Your User-Id is : " + user.Id);
            }
        

        internal void Offerer(Database.Database dBase)
            {
            Console.WriteLine("Welcome As An Offerer > > >");
                Console.WriteLine("Enter Your Username : ");
                string userId = new Exceptions().InputAtLeastOneCharacter();
                User user = new UserServices().GetUserByUserId(dBase, userId);
                if (user != null)
                {
                bool IsApplicant = new BookingServices().IsAApplicant(dBase, user.Id)!=null;
                if(IsApplicant)
                {
                    Console.WriteLine("You Cannot Be A Offerer. . . .");
                    return;
                }
                Offering offerring = new UserServices().GetPreviousCreatedOffer(dBase, user.Id);
                    if (offerring != null)
                    {
                        Console.WriteLine("You Are Already In Service . . .");
                        List<Booking> bookings = new BookingServices().GetAllRequestedBooking(dBase, offerring);
                        if (bookings != null)
                        {
                            int count = 0;
                        foreach(Booking booking in bookings)
                        {
                            Console.WriteLine(++count+". "+ booking.ToString());
                        }
                        count = 0;
                        Console.ReadKey(false);
                        Console.WriteLine("*************************************");
                            foreach (Booking booking in bookings)
                            {
                                if (offerring.Cardetails.LeftSeats == 0)
                                {
                                    Console.WriteLine("No More Request Can Be Procees . . .");
                                    bookings.RemoveAll(element => element.BookingStatus==BookingStatus.REJECTED|| element.BookingStatus==BookingStatus.BROADCAST|| element.BookingStatus==BookingStatus.ACCEPTED);
                                if(bookings.Count!=0)
                                foreach (Booking bookingDestroying in bookings)
                                        bookingDestroying.BookingStatus = BookingStatus.DESTROYED;
                                break;
                                }
                                Console.WriteLine("Applicant Id : " + booking.UserId + "\n" + booking.JourneyDetails + "\n< < < Do you Want To Accept > > >");
                                char select = Console.ReadLine()[0];
                                count++;
                                if (select == 'Y' || select == 'y')
                                {
                                if(booking.BookingStatus == BookingStatus.BROADCAST)
                                {
                                    booking.OffererId = user.Id;
                                }
                                else
                                {
                                    new BookingServices().Accept(dBase, offerring, booking);
                                }
                                    new VechicleService().ProvideASeat(offerring.Cardetails);
                                    new OfferServices().Apply(booking, offerring.Discount);
                                    Console.WriteLine("...Booked...");
                                continue;
                                }
                                else
                                {
                                if(booking.BookingStatus==BookingStatus.PENDING)
                                {
                                    booking.BookingStatus = BookingStatus.REJECTED;
                                    new BookingServices().RejectARequest(booking, offerring.Cardetails);
                                    Console.WriteLine("...Rejected...");
                                }
                                else
                                {
                                    //****************************NOT ACCEPTED**********************
                                }
                            }
                            }
                           
                        Console.WriteLine("People May Be Waiting For You . . .");
                        Console.WriteLine("*************************************");
                    }
                        else
                        {
                            if (offerring.CurrentLocation == offerring.JourneyDetails.Destination)
                            {
                                new BookingServices().DestroyAllPreviousBooking(dBase, offerring, offerring.JourneyDetails.Destination, (offerring.JourneyDetails.Destination - offerring.JourneyDetails.Source > 0));
                                Console.WriteLine("Thank You For Your Suppport...");
                                offerring.Active = false;
                                Console.WriteLine("Your Total Earning Is : " + offerring.TotalEarning);
                            }
                            else
                            {
                                Console.WriteLine("You are already in service . . .");
                                Console.WriteLine("Do You Reach Your Destination . . .");
                                char IsReach = Console.ReadLine()[0];
                                if (IsReach == 'Y' || IsReach == 'y')
                                {
                                    new BookingServices().DestroyAllPreviousBooking(dBase, offerring, offerring.JourneyDetails.Destination, (offerring.JourneyDetails.Destination - offerring.JourneyDetails.Source) > 0);
                                    Console.WriteLine("Thank You For Your Suppport...");
                                    offerring.Active = false;
                                    Console.WriteLine("Your Total Earning Is : " + offerring.TotalEarning);
                                }
                                else
                                {
                                    Console.WriteLine("Your Current Location Is : " + Enum.GetName(typeof(Address), offerring.CurrentLocation));

                                }

                            }
                        }

                    }
                    else
                    {
                        Offering offering = new OfferServices().Create(dBase, user.Id);
                        offering.Active = true;
                        Console.WriteLine("Welcome," + user.Name + "\nEnter Your Car Details->>\nEnter Number Plate : ");
                        string carNumber = new Exceptions().InputAtLeastOneCharacter();

                        Console.WriteLine("Enter Maximum Seats Of Your Car : ");
                        int maxSeats = new Exceptions().InputOnlyInteger();
                        Console.WriteLine("Enter Number Of Seats You Want To Pool.");
                        int numOfSeatsToPool = new Exceptions().InputOnlyInteger(); ;

                    while (numOfSeatsToPool>maxSeats)
                    {
                        Console.WriteLine("Please, Enter Less Seats To Offfer . . .");
                        numOfSeatsToPool = new Exceptions().InputOnlyInteger();
                    }
                        offering.Cardetails.MaxOfferSeats = numOfSeatsToPool;
                        offering.Cardetails.LeftSeats = numOfSeatsToPool;
                        offering.Cardetails.Number = carNumber;
                        offering.Cardetails.MaxSeats = maxSeats;
                        Console.WriteLine("Enter Your Start Point : ");
                        for (int i = 1; i <= Enum.GetValues(typeof(Address)).Length; i++)
                            Console.WriteLine((i) + ". " + Enum.GetName(typeof(Address), i));

                        Console.WriteLine("Enter your source : ");
                        int choices;
                        while (true)
                        {
                            choices = Int32.Parse(Console.ReadLine());
                            if (choices <= Enum.GetValues(typeof(Address)).Length)
                                break;
                            else
                            {
                                Console.WriteLine("Please, Enter A Valid Choice : ");
                                continue;
                            }
                        }
                        offering.JourneyDetails.Source = (Address)(choices);
                        offering.CurrentLocation = offering.JourneyDetails.Source;
                        Console.WriteLine("Enter your destination : ");
                        while (true)
                        {
                            choices = Int32.Parse(Console.ReadLine());
                            if (choices <= Enum.GetValues(typeof(Address)).Length && choices!=(int)offering.JourneyDetails.Source)
                                break;
                            else
                            {
                                Console.WriteLine("Please, Enter A Valid Choice : ");
                                continue;
                            }
                        }
                        offering.JourneyDetails.Destination = (Address)(choices);
                        Console.WriteLine("Enter Fare Price Per Kilometer : ");

                        float price;
                        while (true)
                        {

                            try
                            {
                                price = float.Parse(Console.ReadLine());
                                break;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Please, Enter A Decimal Value : ");
                                continue;
                            }
                        }
                        offering.JourneyDetails.Price = price;
                        Console.WriteLine("Choose A Offer :\n-->1.5% DISCOUNT\n-->2.10% DISCOUNT\n-->3.20% DISCOUNT");
                        char offerChoice = Console.ReadLine()[0];
                        if (offerChoice == '2')
                            offering.Discount = Discount.DISCOUNT_10P;
                        else if (offerChoice == '3')
                            offering.Discount = Discount.DISCOUNT_20P;
                        else
                            offering.Discount = Discount.DISCOUNT_5P;

                    }
                }
                else
                    Console.WriteLine("No Such Account Exists!!!");
            }
            
        internal void Applicant(Database.Database dBase)
        {
            Console.WriteLine("Welcome As An Applicant > > >");
            Console.WriteLine("Enter Your Username : ");
                string userId = Console.ReadLine();
                User user = new UserServices().GetUserByUserId(dBase, userId);
                ConsoleKeyInfo key;
                
            if (user != null )
                {
                bool IsOfferer = new OfferServices().GetByUserId(dBase, user.Id) != null;
                if (IsOfferer)
                {
                    Console.WriteLine("You Cannot Be An Applicant . . .");
                    return;
                }
                Booking booking = new UserServices().GetBookingByUserId(dBase, user.Id);
                    if (booking != null && booking.BookingStatus == BookingStatus.PENDING)
                    {
                        Console.WriteLine("You Are In Pending . . .\n<<Do You Want To Cancel(Y/N)>>");
                        key = Console.ReadKey(true);
                        if (key.KeyChar == 'Y' || key.KeyChar == 'y')
                        {
                            Offering offerer = new OfferServices().GetByUserId(dBase, booking.OffererId);
                            new VechicleService().DedeuctASeat(offerer.Cardetails);
                            offerer.TotalEarning -= booking.JourneyDetails.Price;
                        booking.BookingStatus = BookingStatus.NOTREQUESTED;
                            Console.WriteLine("Choose A New Offer Next Time! . . .");
                            return;
                        }

                        new BookingServices().DestroyAllPreviousBooking(dBase, new UserServices().GetPreviousCreatedOffer(dBase, booking.OffererId),
                        new UserServices().GetPreviousCreatedOffer(dBase, booking.OffererId).CurrentLocation, (booking.JourneyDetails.Destination - booking.JourneyDetails.Source > 0));
                        if (dBase.Bookings.Contains(booking))
                        {
                            if (new UserServices().GetPreviousCreatedOffer(dBase, booking.OffererId).Cardetails.LeftSeats == 0)
                            {
                                Console.WriteLine("No Space,Aborting Your Request. . .");
                                booking.BookingStatus = BookingStatus.DESTROYED;
                            }
                            else
                            {
                                Console.WriteLine("Wait For The Guy To Accept Your Request . . .");
                            }

                        }
                        else
                        {
                            Console.WriteLine("Sorry, Your Request Is Discarded . . .");
                            new BookingServices().Removed(booking);
                            Console.WriteLine("Check other Offers Next Time. . .");
                           
                        }
                    }
                    else if (booking != null && booking.BookingStatus == BookingStatus.DESTROYED)
                    {
                        Console.WriteLine("Your Fare Was : " + booking.JourneyDetails.Price);
                        new BookingServices().Removed(booking);
                    }
                    else if (booking != null && new UserServices().GetBookingByUserId(dBase, user.Id).BookingStatus == BookingStatus.ACCEPTED)
                    {
                        Console.WriteLine("Your Request Is Accepted . . .");
                        User offerer = new UserServices().GetUserByUserId(dBase, booking.OffererId);
                        Console.WriteLine("You Have Been  Accepted By : " + offerer.Name);
                        Console.WriteLine("Did You Reach Your Destination(Y/N) : ");
                        char select = Console.ReadLine()[0];
                        if (select == 'y' || select == 'Y')
                        {
                            Offering offerring = new UserServices().GetPreviousCreatedOffer(dBase, booking.OffererId);
                            if (booking.JourneyDetails.Destination >= offerring.CurrentLocation)
                                offerring.CurrentLocation = booking.JourneyDetails.Destination;
                            offerring.TotalEarning += booking.JourneyDetails.Price;
                            new VechicleService().DedeuctASeat(offerring.Cardetails);
                            Console.WriteLine("Your Fare Price Is : " + booking.JourneyDetails.Price);
                            new BookingServices().DestroyAllPreviousBooking(dBase, offerring, booking.JourneyDetails.Destination, (booking.JourneyDetails.Destination - booking.JourneyDetails.Source > 0));

                        }
                    }
                    else if (booking != null && booking.BookingStatus == BookingStatus.REJECTED)
                    {
                        Console.WriteLine("Your Request Was Rejected . . .Try Some Other Offer Again");
                        new BookingServices().Removed(booking);
                    }
                else if (booking != null && booking.BookingStatus == BookingStatus.BROADCAST)
                {
                    if(booking.OffererId!=null)
                    {
                        Console.WriteLine("Do You Wnat To Cancel Tour Accepted Request . . .");
                        char decision = Console.ReadLine()[0];
                        if(decision=='Y'||decision=='y')
                        {
                            new BookingServices().CancelABookingByOffererId(dBase, booking.OffererId, booking);
                        }
                        else
                        {
                            booking.BookingStatus = BookingStatus.ACCEPTED;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Your Are Still In BroadCast, Wait Or Cancel ?\n1.Wait\t2.Cancel ");
                        char decision = Console.ReadLine()[0];
                        if (decision == '1')
                        {
                            Console.WriteLine("Please, Wait For Some User");
                        }
                        else
                            new BookingServices().Removed(booking);
                    }
                }
                else
                    {
                        for (int i = 1; i <= Enum.GetValues(typeof(Address)).Length; i++)
                            Console.WriteLine((i) + ". " + Enum.GetName(typeof(Address), i));
                        Console.WriteLine("Enter your source : ");
                        int selectRoute;
                        while (true)
                        {
                            selectRoute = Int32.Parse(Console.ReadLine());
                            if (selectRoute <= Enum.GetValues(typeof(Address)).Length)
                                break;
                            else
                            {
                                Console.WriteLine("Please, Enter A Valid Choice : ");
                                continue;
                            }
                        }
                        Address Source = (Address)(selectRoute);
                        Console.WriteLine("Enter your destination : ");
                        while (true)
                        {
                            selectRoute = Int32.Parse(Console.ReadLine());
                            if (selectRoute!=(int)Source && selectRoute <= Enum.GetValues(typeof(Address)).Length)
                                break;
                            else
                            {
                                Console.WriteLine("Please, Enter A Valid Choice : ");
                                continue;
                            }
                        }
                        Address Destination = (Address)(selectRoute);
                    Console.WriteLine("1.Book A Ride\t2.View A Ride\tE.Exit");
                    char applicantChoice = Console.ReadLine()[0];
                    List<Offering> offerrings = new OfferServices().GetAllOffersWithinReach(dBase, Source, Destination);
                    switch (applicantChoice)
                    {
                        case '1':
                            {
                                if (offerrings == null)
                                {
                                    Console.WriteLine("No Pool Exists!!!");
                                    Console.WriteLine("Do You Want To Pass A Ride?(Y/N)");
                                    char decision = Console.ReadLine()[0];
                                    if(decision=='y'||decision=='Y')
                                    {
                                        new BookingServices().PassBooking(dBase, user, Source, Destination);
                                        Console.WriteLine("Wait For Some Offerer To Accept . . .");
                                        break ;
                                    }
                                        Console.WriteLine("Logging Out !!!");
                                        return;
                                    
                                }
                                // *********************************************
                                foreach (Offering offering in offerrings)
                                {
                                    User tempUser = new UserServices().GetUserByUserId(dBase, offering.UserId);
                                    Console.WriteLine(offering.UserId + "\nName : " + tempUser.Name + " " + offering.ToString());
                                }
                                Console.WriteLine("Enter The UserId Of Offerer : ");
                                string pollerUserid = Console.ReadLine();
                                Offering offer = new UserServices().GetPreviousCreatedOffer(dBase, pollerUserid);
                                bool haveSpace = new VechicleService().ProvideASeat(offer.Cardetails);
                                if (haveSpace)
                                {
                                    if (offer != null)
                                    {
                                        new BookingServices().Book(dBase, offer, Source, Destination, user.Id);
                                        Console.WriteLine("Booked A Car,You are in pending, wait for few minutes to be accepted by the " + new UserServices().GetUserByUserId(dBase, pollerUserid).Name);
                                    }
                                    else
                                        Console.WriteLine("Poller Do Not Exists");
                                }
                                else
                                {
                                    new BookingServices().Book(dBase, offer, Source, Destination, user.Id);
                                    Console.WriteLine("Wait,Or Cancel The Reuest . . .");
                                }
                            }
                            break;
                        case '2':
                            if (offerrings != null)
                            {
                                foreach (Offering offering in offerrings)
                                {
                                    User tempUser = new UserServices().GetUserByUserId(dBase, offering.UserId);
                                    Console.WriteLine(offering.UserId + "\nName : " + tempUser.Name + " " + offering.ToString());
                                }
                                Console.ReadKey(false);
                            }
                            else
                                Console.WriteLine("No Pool Exists !!!");
                            break;
                        default:
                            Console.WriteLine("Logging Out");
                            return;
                    }
                  }
                }

                else
                    Console.WriteLine("No Such Account Exists!!!");
            }


    }
}
