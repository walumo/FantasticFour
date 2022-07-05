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
            // Asking user choice

            string departureTown = UserInputs.GetStringInput();
            string arrivalTown = UserInputs.GetStringInput();

            // korjaa jos käyttäjä syöttää pienemmän tai suuremman kun 1-6




            while (true)
            {
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
                        break;
                    case UserOptions.Departures:
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

            NetworkConnection.InitializeClient();

            JsonClient client = new JsonClient();

            var trains = await client.GetDataAsync<List<Train>>("/trains/latest/5");

            trains.ForEach((train) => Console.WriteLine());
        }
    }
}
