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

            while (true)
            {

                Console.Write("Enter date (dd.mm.yyyy): ");
                string str = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(str)) return DateTime.Now;
                else
                {
                    try
                    {
                        string[] dtParser = new string[3];
                        dtParser = str.Split('.');
                        Console.Clear();
                        return new DateTime(Convert.ToInt32(dtParser[2]), Convert.ToInt32(dtParser[1]), Convert.ToInt32(dtParser[0]));
                    }
                    catch (Exception e1)
                    {
                        Console.WriteLine("Not a valid date! Use dd.mm.yyyy...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

            }
        }
    }
}
