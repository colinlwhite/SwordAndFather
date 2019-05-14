using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwordAndFather.Models
{
    public class Assassin
    {
        // Creating properties 
        public int Id { get; set; }
        public string CodeName { get; set; }
        public string CatchPhrase { get; set; }
        public string PreferredWeapon { get; set; }
        public int Rating { get; set; }
        public decimal StandardFee { get; set; }

        // Make a constructor method so we can instantiate it - these things are required when instianitating the object
        public Assassin(string codeName, string catchphrase, string preferredWeapon)
        {
            CodeName = codeName;
            CatchPhrase = catchphrase;
            PreferredWeapon = preferredWeapon;
        }
    }
}
