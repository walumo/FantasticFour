using FantasticFour.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FantasticFour.bloc
{
    static public class Show
    {
        public static void Features(RootobjectFeatures train, Options options)
        {
            while (true)
            {
                Console.Clear();
                RefreshFeatures(train, options);
                Console.WriteLine("\nShowing train no: {0} features on {1}", options.TrainNumber, options.Date.ToShortDateString());
                Console.Write("Press any key to exit...");
                var input = Console.ReadKey();
                if (input.Key != ConsoleKey.Escape) break;
            }
        }

        public static void DepartingTrains(List<Train> list, Options options)
        {
            while (true)
            {
                Console.Clear();
                try
                {
                    RefreshDeparting(list, options);
                }
                catch (Exception exDep)
                {
                    File.AppendAllText("errorLog.txt", DateTime.Now + "| error: Could not get Wagon features \n" + exDep.ToString() + "\n\n");
                    Console.WriteLine("\nSomething went wrong: No data to display.\n");
                }
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
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(" {0, -10} {1, -10} {2, -10} {3, -20} {4, -10} {5, -10}", "Train ID", "Type", "Arriving", "At", "From", "Late");
                Console.BackgroundColor = ConsoleColor.Black;
                RefreshArriving(list, options).Wait();
                Console.WriteLine("\nShowing next 25 trains arriving to {0}", options.DestinationStation, options.Date.ToShortDateString());
                Console.Write("Press any key to exit...");
                var input = Console.ReadKey();
                if (input.Key != ConsoleKey.Escape) return;
            }
        }
        internal static void RefreshDeparting(List<Train> list, Options options)
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" {0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}", "Train ID", "Type", "Departing", "At", "Arriving", "At");
            Console.BackgroundColor = ConsoleColor.Black;

            list = list.Where(x => x.trainCategory == "Commuter" || x.trainCategory == "Long-distance").ToList();

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
        internal static async Task RefreshArriving(List<Train> list, Options options)
        {
            foreach (Train train in list)
            {
                int index = default;
                foreach (Timetablerow ttr in train.timeTableRows)
                {
                    if (ttr.stationShortCode == options.DestinationStation)
                    {
                        index = ttr.stationShortCode.IndexOf(options.DestinationStation);
                    }
                }
                int trainId = train.trainNumber;
                string trainType = train.trainType;
                string destination = options.DestinationStation;
                string from = "";

                RootobjectFeatures getFrom = await Metodit.RouteEndpoints(options.Date, train.trainNumber);

                try
                {
                    from = getFrom.journeySections[0].beginTimeTableRow.stationShortCode;

                }
                catch (Exception)
                {
                    from = "N/A";
                }

                DateTime arrives = (from x in train.timeTableRows
                                    where x.stationShortCode == destination
                                    select x.scheduledTime).First();
                string late = "";
                if (DateTime.Now - train.timeTableRows[index].liveEstimateTime < DateTime.Now - train.timeTableRows[index].scheduledTime)
                {
                    late = "Late";
                    Console.Write(" {0, -10} {1, -10} {2, -10} {3, -20} {4, -10}", trainId, trainType, destination, arrives.ToString("HH:mm dd:MM:yyyy"), from);
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write(" {0, -10}\n", late);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    late = "On time";
                    Console.Write(" {0, -10} {1, -10} {2, -10} {3, -20} {4, -10}", trainId, trainType, destination, arrives.ToString("HH:mm dd:MM:yyyy"), from);
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write(" {0, -10}\n", late);
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }


        }
        internal static void RefreshFeatures(RootobjectFeatures train, Options options)
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(" {0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -5}", "Train ID", "Type", "Catering", "Playground", "Disabled", "Pet");
            Console.BackgroundColor = ConsoleColor.Black;


            string catering = "";
            string playground = "";
            string disabled = "";
            string pet = "";

            try
            {
                foreach (Wagon wagon in train.journeySections[0].wagons)
                {
                    if (String.IsNullOrWhiteSpace(catering) || catering == "No")
                    {
                        catering = wagon.catering ? "Yes" : "No";
                    }
                    if (String.IsNullOrWhiteSpace(playground) || playground == "No")
                    {
                        playground = wagon.playground ? "Yes" : "No";
                    }
                    if (String.IsNullOrWhiteSpace(disabled) || disabled == "No")
                    {
                        disabled = wagon.disabled ? "Yes" : "No";
                    }
                    if (String.IsNullOrWhiteSpace(pet) || pet == "No")
                    {
                        pet = wagon.pet ? "Yes" : "No";
                    }
                }

                Console.WriteLine(" {0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -10}",
                    train.trainNumber,
                    train.trainType,
                    catering,
                    playground,
                    disabled,
                    pet
                    );
            }
            catch (Exception exFeatures)
            {
                File.AppendAllText("errorLog.txt", DateTime.Now + "| error: Could not get Wagon features \n" + exFeatures.ToString() + "\n\n");
                Console.WriteLine("\nSomething went wrong: No data to display.\n");
            }
        }
    }
}
