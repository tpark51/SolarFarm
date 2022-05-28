using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.UI
{
    public class Validation
    {
        private static void Prompt2Continue()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine("Press any key to continue...");

            Console.ReadLine();
        }

        internal static string PromptRequired(string message)
        {
            var res = PromptUser(message);
            while (string.IsNullOrEmpty(res))
            {
                Console.WriteLine("Input required❗");
                res = PromptUser(message);
            }

            return res;
        }

        internal static string PromptUser(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }

        internal static decimal PromptUser4Num(string message)
        {
            decimal result;
            while (!decimal.TryParse(PromptUser(message), out result))
            {
                PromptUser("Invalid Input. Press Enter to Continue");
            }

            return result;
        }

        internal static int PromptUser4Int(string message, int min, int max)
        {
            int result;
            while (!int.TryParse(PromptUser(message), out result) || result < min || result > max)
            {
                PromptUser($@"Invalid Input, must be between {min} and {max}
Press Enter to Continue");
            }

            return result;
        }

        // default here means it takes the absolute minimum value for a DateTime
        internal static DateTime PromptUser4Date(string message, DateTime max)
        {
            DateTime result;
            while (!(DateTime.TryParse(PromptUser(message), out result)) || (result > max))
            {
                PromptUser($"Invalid Input, must be before {max}.");
                Prompt2Continue();
            }

            return result;
        }

        internal static int PromptUser4EnumInt(string message)
        {
            int enumInt = int.Parse(PromptUser(message));

            while (enumInt < 1 || enumInt > 5)
            {
                PromptUser("Invalid input.");
                break;


            }

            return enumInt;
        }
        internal static DateTime PromptUserForDateYear(string message)
        {
            bool isValid = DateTime.TryParse(PromptUser(message), out DateTime date);
            while (!isValid)
            {
                Console.WriteLine("Not a valid date format, please try again");
                isValid = DateTime.TryParse(Console.ReadLine(), out date);
            }
            return date;
        }

    }

}

