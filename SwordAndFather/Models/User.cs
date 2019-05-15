using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather.Models
{
    public class User
    {
        //public User(string username, string password)
        //{
          //  UserName = username;
           // Password = password;
        //}

            // If I get an error, I can delete the constructor, add all of the properties to the instructor or create an empty constructor

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // POCO - Plain Old CLR Objects - is what is above

        public List<Target> Targets { get; set; }



    }
}
