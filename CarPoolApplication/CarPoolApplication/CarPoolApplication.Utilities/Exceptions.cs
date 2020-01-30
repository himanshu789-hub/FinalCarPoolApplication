using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Utilities
{
   public class Exceptions
    {
        public string InputAtLeastOneCharacter()
        {
            try
            {
               string input =  Console.ReadLine();
                if (input.Length == 0)
                    throw new Exception();
                else
                    return input;
            }
            catch(Exception e)
            {
               return InputAtLeastOneCharacter();
            }
        }

        public int InputOnlyInteger()
        {
            try
            {
                int input = Int32.Parse(Console.ReadLine());
                return input;
            }
            catch(Exception e)
            {
                return InputOnlyInteger();
            }
        }
    }
}
