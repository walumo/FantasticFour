using FantasticFour.bloc;
using FantasticFour.models;
using FantasticFour.network;
using System;
using System.Threading.Tasks;

namespace FantasticFour
{

    static class Program
    {

        enum UserOptions
        {
            Exit,
            Departures,
            Arrivals,
            Features,
            Options
        };

        static async Task Main(string[] args)
        {
            // Luodaan options -olio ja annetaan sille vakioarvot. 
            Options options = new Options(departure: "HKI", destination: "TKU", date: DateTime.Now, trainNumber: 1);

            //Http-clientin alustaminen. 
            NetworkConnection.InitializeClient();

            while (true)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Train scheduler 1.0.0\n");
                Console.BackgroundColor = ConsoleColor.Black;

                Console.WriteLine("Choose 0 to exit");
                Console.WriteLine("Choose 1 to show route schedules");
                Console.WriteLine("Choose 2 to show arrivals on selected destination");
                Console.WriteLine("Choose 3 to list features of selected train");
                Console.WriteLine("Choose 4 to edit locations, date and train ID");

                Console.Write("\nSelect your option: ");
                string userChoice = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(userChoice) || !int.TryParse(userChoice, out int result)) continue; 

                switch ((UserOptions)Convert.ToInt32(userChoice))
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
                    case UserOptions.Features:
                        await Metodit.Features(options);
                        break;
                    case UserOptions.Options:
                        options = await Search.ShowOptionsMenu(options);
                        break;
                }
            }
        }
    }
}
