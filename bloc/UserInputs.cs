using System;
using System.Collections.Generic;
using System.Text;

namespace FantasticFour.bloc
{
    public class UserInputs
    {
        public static string GetStringInput()
        {
            string input = Console.ReadLine();
            return input;
        }

        public static int GetIntInput()
        {
            int input;
            while (true)
            {
                try
                {
                    input = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form");
                    continue;
                }
                break;
            }
            return input;
        }

        public static DateTime GetDepDate()
        {

            DateTime date = new DateTime();
            while (true)
            {
                try
                {
                    Console.Write("Enter date (yyyy/mm/dd): ");
                    date = Convert.ToDateTime(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Give input in correct form, press any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                break;
            }
            return date;
        }
    }
}
