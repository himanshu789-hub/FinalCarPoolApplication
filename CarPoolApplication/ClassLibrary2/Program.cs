using System;
using System.Collections.Generic;
namespace CarPoolApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.Database dBase = new Database.Database();
            while(true)
            {
                Console.WriteLine("\t<<CAR POOLING SERVICE>>\nWELCOME");
                Console.WriteLine("-->1.SignUp\n-->2.LogIn As Offerer\n-->3.LogIn As Applicant\n-->E.Exit");
                ConsoleKeyInfo key = Console.ReadKey(true);
                char homeChoice = key.KeyChar;
                switch (homeChoice)
                {
                    case '1':
                        new Routine().SignUp(dBase);
                        break;
                    case '2':
                        new Routine().Offerer(dBase);
                        break;
                    case '3':
                        new Routine().Applicant(dBase);
                        break;
                    case 'E':
                    case 'e':
                        {
                            Console.WriteLine("Loging Out");
                            continue;
                        }
                    default:
                        Console.WriteLine("Enter a valid Input");
                        continue;
                }
            }
        }
    }
}
