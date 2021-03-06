using System;

namespace FantasticFour.bloc
{
    public static class UserInputs
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
                    Console.Write("Give input in correct form");
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
                    catch (Exception)
                    {
                        Console.WriteLine("Not a valid date! Use dd.mm.yyyy...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }

            }
        }
        public static int GetTrainNumber()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Enter train number: ");
                string input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input)) return default;
                if (!String.IsNullOrWhiteSpace(input) && int.TryParse(input, out int result) && result <= 11111 && result > 0) return result;
                else
                {
                    Console.WriteLine("Invalid train number! Enter between 1-9999...");
                    Console.ReadKey();
                }
            }
        }
    }
}