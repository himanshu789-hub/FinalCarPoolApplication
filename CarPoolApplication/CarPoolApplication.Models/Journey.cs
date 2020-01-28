using System;
using System.Collections.Generic;
using System.Text;
namespace CarPoolApplication.Models
{
 public class Journey
    {
        public Address Source;
        public Address Destination;
        public float Price;
        public int Distance;//in km

        public override string ToString()
        {
 return "Source : " + Enum.GetName(typeof(Address),Source)+"\nDestination : "+Enum.GetName(typeof(Address),Destination)+"\nTotal Fare : "+Price;
        }


    }
}
