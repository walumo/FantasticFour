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
        // Valikon vaihtoehdot.
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
            // Luodaan options -olio ja annetaan sille vakioarvot. 
            Options options = new Options(departure: "HKI", destination: "TKU", date: DateTime.Now);
            
            //Http-clientin alustaminen. 
            NetworkConnection.InitializeClient();

            while (true)
            {   
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Train scheduler 1.0.0\n");
                Console.BackgroundColor = ConsoleColor.Black;

                Console.WriteLine("Choose 0 to exit.");
                Console.WriteLine("Choose 1 to show route schedules");
                Console.WriteLine("Choose 2 to show arrivals on selected destination");
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
                        await Metodit.Departure(options);
                        break;
                    case UserOptions.Arrivals:
                        await Metodit.Arrivals(options);
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
