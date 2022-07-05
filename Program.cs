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
            DepTown,
            ArrTown
        };


        static async Task Main(string[] args)
        {
            NetworkConnection.InitializeClient();
            while (true)
            {
                string stationCode = await Search.GetStationName();
                Console.WriteLine("Selected station: "+stationCode);
                Console.ReadKey();
            }
            NetworkConnection.InitializeClient();
            // Asking user choice
            Console.WriteLine("Enter departure town: ");
            string departureTown = UserInputs.GetStringInput();

            Console.WriteLine("Enter arrival town: ");
            string arrivalTown = UserInputs.GetStringInput();
            Console.WriteLine("Enter departure date: ");
            DateTime date = UserInputs.GetDepDate();
            // korjaa jos käyttäjä syöttää pienemmän tai suuremman kun 1-6




            while (true)
            {
                Console.WriteLine("Choose 0 to exit.");
                Console.WriteLine("Choose 1 to find out departure of next train");
                Console.WriteLine("Choose 2 to find out arrival of next train");
                Console.WriteLine("Choose 3 to find out if selected train is late");
                Console.WriteLine("Choose 4 to list features of selected train");
                Console.WriteLine("Choose 5 to edit location of departure town");
                Console.WriteLine("Choose 6 to edit location of arrival town");

                int userChoice = UserInputs.GetIntInput();

                switch ((UserOptions)userChoice)
                {
                    case UserOptions.Exit:
                        Environment.Exit(0);
                        break;
                    case UserOptions.Departures:
                        await Metodit.Departure(departureTown, arrivalTown, date);
                        break;
                    case UserOptions.Arrivals:
                        break;
                    case UserOptions.Schedule:
                        break;
                    case UserOptions.Features:
                        break;
                    case UserOptions.DepTown:
                        break;
                    case UserOptions.ArrTown:
                        break;
                }
            }
        }
    }
}
