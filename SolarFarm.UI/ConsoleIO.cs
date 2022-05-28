using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.UI
{
    public class ConsoleIO
    {
        public static void Display(string message)
        {
            Console.WriteLine(message);
        }
        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Display(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Display(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static int DisplayMenu()
        {
            return (int)Validation.PromptUser4Int(@"
Main Menu
=========
0. Exit
1. Find Panels by Section
2. Add a Panel
3. Update a Panel
4. Remove a Panel
Select [0-4]: ", 0, 4);
        }
        public static string DisplayFindMessage()
        {
            return Validation.PromptUser(@"
Find Panel by Section
=======================

Enter Section Name: ");
        }

        public static string DisplayRemoveMessage()
        {
            return Validation.PromptUser(@"
Remove A Panel
=======================

Enter Section Name: ");
        }

     

    }
}