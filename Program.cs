using System;
using FantasticFour.models;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using FantasticFour.network;
using FantasticFour.bloc;



namespace FantasticFour
{

    static class Program
    {

        enum UserOptions
        {
            Exit,
            Departures,
            Arrivals,
            Schedule,
            Features,
            Options
        };

        static async Task Main(string[] args)
        {
            Options options = new Options(departure: "HKI", destination: "TKU", date: DateTime.Now);
            NetworkConnection.InitializeClient();

            while (true)
            {   
                //Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Train scheduler 1.0.0\n");
                Console.BackgroundColor = ConsoleColor.Black;

                Console.WriteLine("Choose 0 to exit.");
                Console.WriteLine("Choose 1 to find out departure of next train");
                Console.WriteLine("Choose 2 to find out arrival of next train");
                Console.WriteLine("Choose 3 to find out if selected train is late");
                Console.WriteLine("Choose 4 to list features of selected train");
                Console.WriteLine("Choose 5 to edit locations and date");

                Console.Write("\nSelect your option: ");
                int userChoice = UserInputs.GetIntInput();

                switch ((UserOptions)userChoice)
                {
                    case UserOptions.Exit:
                        Environment.Exit(0);
                        break;
                    case UserOptions.Departures:
                        await Metodit.Departure(options.DepartureStation, options.DestinationStation, options.Date);
                        break;
                    case UserOptions.Arrivals:
                        await Metodit.Arrivals(options.DestinationStation);
                        break;
                    case UserOptions.Schedule:
                        break;
                    case UserOptions.Features:
                        break;
                    case UserOptions.Options:
                        Console.Clear();
                        options = await Search.ShowOptionsMenu(options);
                        break;
                }
            }
        }
    }
}
