using System;
using System.Collections.Generic;
using System.Text;

namespace CarPoolApplication.Models
{
    public class User
    {
        string password;
        public string Name;
        public string Id;
        public int Age;
        public Gender gender;
        
        public User(string password,string name)
        {
            this.password = password;
            Name = name;
            Id = name.Substring(0, 3) + DateTime.UtcNow.ToString().Replace(" ","");
        }

        public bool IsPasswordValid(string password)
        {
            return this.password.Equals(password);
        }
    }
}
