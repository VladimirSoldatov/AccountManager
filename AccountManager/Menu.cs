using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager
{
    internal class Menu
    {
        string[] menu = new string[] { "ReadAll", "ReadAndWriteAll", "ReadMain", "Exit" };//Гость читал ReadMain, Exit; пользователь как Гость + ReadAll, Администратор - все Позиции
        public Menu(int mandatory)
        {
            for (int i = 0; i<menu.Length; i++)
            {
                if(!(mandatory - i <-1))
                {
                    Console.WriteLine(menu[i]);
                }
            }
        }
    }
}
