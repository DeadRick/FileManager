using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    internal partial class Program
    {
        /// <summary>
        /// Интро.
        /// </summary>
        public static void Intro()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\t\t\t ____  _   _     ____      _       __    _       __    __    ____  ___  " + "\n" +
                    "\t\t\t" + @"| |_  | | | |   | |_      | |\/|  / /\  | |\ |  / /\  / /`_ | |_  | |_)" + "\n" +
                    "\t\t\t" + @"|_|   |_| |_|__ |_|__     |_|  | /_/--\ |_| \| /_/--\ \_\_/ |_|__ |_| \" + "\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\t\t\t\t      -|             Добро пожаловать          |-");
            Console.WriteLine("\t\t\t\t     --|                    в                  |--");
            Console.WriteLine("\t\t\t\t    ---|            Файловый Менеджер          |---");
            Console.WriteLine("\t\t\t\t     --|         help список всех команд       |--");
            Console.WriteLine("\t\t\t\t      -|                                       |-\n");
            Console.ResetColor();
          
        }
    }
}
