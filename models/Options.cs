using System;

namespace FantasticFour.models
{
    public struct Options
    {
        public string DepartureStation { get; set; }
        public string DestinationStation { get; set; }
        public DateTime Date { get; set; }
        public int TrainNumber { get; set; }
        public Options(string departure, string destination, DateTime date, int trainNumber)
        {
            DepartureStation = departure;
            DestinationStation = destination;
            Date = date;
            TrainNumber = trainNumber;
        }
    }
}
