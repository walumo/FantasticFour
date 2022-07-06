using FantasticFour.models;
using FantasticFour.network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;
using System.Threading;

namespace FantasticFour.bloc
{
    public class Show
    {
        public static void DepartingTrains(List<Train> list, Options options)
        {
            while (true)
            {
                Console.Clear();
                RefreshDeparting(list, options);
                Console.WriteLine("\nShowing departing trains from {0} to {1} at {2}", options.DepartureStation, options.DestinationStation, options.Date.ToShortDateString());
                Console.Write("Press any key to exit...");
                var input = Console.ReadKey();
                if (input.Key != ConsoleKey.Escape) break;
            }

        }        
        public static void ArrivingTrains(List<Train> list, Options options)
        {
            while (true)
            {
                Console.Clear();
                RefreshArriving(list, options);
                Console.WriteLine("\nShowing trains arriving to {0} on {1}",options.DestinationStation, options.Date.ToShortDateString());
                Console.Write("Press any key to exit...");
                var input = Console.ReadKey();
                if (input.Key != ConsoleKey.Escape) break;
            }

        }
        internal static void RefreshDeparting(List<Train> list, Options options)
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" {0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "Train ID","Type", "Departing", "At", "Arriving", "At");
            Console.BackgroundColor = ConsoleColor.Black;

            foreach (Train train in list)
            {
                int trainId = train.trainNumber;
                string trainType = train.trainType;
                string departing = options.DepartureStation;
                string destination = options.DestinationStation;
                DateTime leaves = (from x in train.timeTableRows where x.stationShortCode == departing select x.scheduledTime).First();
                DateTime arrives = (from x in train.timeTableRows where x.stationShortCode == destination select x.scheduledTime).First();

                Console.WriteLine(" {0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", trainId, trainType, departing, leaves.ToString("HH:mm"), destination, arrives.ToString("HH:mm"));
            }
        }        
        internal static void RefreshArriving(List<Train> list, Options options)
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" {0, -10} {1, -10} {2, -10} {3, -20}", "Train ID","Type", "Arriving", "At");
            Console.BackgroundColor = ConsoleColor.Black;

            foreach (Train train in list)
            {
                int trainId = train.trainNumber;
                string trainType = train.trainType;
                string destination = options.DestinationStation;
                DateTime arrives = (from x in train.timeTableRows
                                    where x.stationShortCode == destination
                                    select x.scheduledTime).First();

                Console.WriteLine(" {0, -10} {1, -10} {2, -10} {3, -20}", trainId, trainType, destination, arrives.ToString("HH:mm dd:MM:yyyy"));
            }
        }
    }
}
