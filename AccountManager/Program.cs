using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PasswordManager ac = new PasswordManager("passwords.txt");
            ac.Authentication("Sadmin", "P@$$word");
            Menu m = new Menu(ac.MandatoryStatus());
        }
    }
}
