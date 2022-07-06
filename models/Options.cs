using System;

namespace FantasticFour.models
{
    public struct Options
    {
        public string DepartureStation { get; set; }
        public string DestinationStation { get; set; }
        public DateTime Date { get; set; }
        public Options(string departure, string destination, DateTime date)
        {
            DepartureStation = departure;
            DestinationStation = destination;
            Date = date;
        }
    }
}
