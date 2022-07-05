using System;

namespace FantasticFour.models
{

    public class RootobjectFeatures
    {
        public int trainNumber { get; set; }
        public string departureDate { get; set; }
        public int operatorUICCode { get; set; }
        public string operatorShortCode { get; set; }
        public string trainCategory { get; set; }
        public string trainType { get; set; }
        public long version { get; set; }
        public Journeysection[] journeySections { get; set; }
    }

    public class Journeysection
    {
        public Begintimetablerow beginTimeTableRow { get; set; }
        public Endtimetablerow endTimeTableRow { get; set; }
        public Locomotive[] locomotives { get; set; }
        public Wagon[] wagons { get; set; }
        public int totalLength { get; set; }
        public int maximumSpeed { get; set; }
        public int attapId { get; set; }
        public int saapAttapId { get; set; }
    }

    public class Begintimetablerow
    {
        public string stationShortCode { get; set; }
        public int stationUICCode { get; set; }
        public string countryCode { get; set; }
        public string type { get; set; }
        public DateTime scheduledTime { get; set; }
    }

    public class Endtimetablerow
    {
        public string stationShortCode { get; set; }
        public int stationUICCode { get; set; }
        public string countryCode { get; set; }
        public string type { get; set; }
        public DateTime scheduledTime { get; set; }
    }

    public class Locomotive
    {
        public int location { get; set; }
        public string locomotiveType { get; set; }
        public string powerType { get; set; }
    }

    public class Wagon
    {
        public string wagonType { get; set; }
        public int location { get; set; }
        public int salesNumber { get; set; }
        public int length { get; set; }
        public bool playground { get; set; }
        public bool disabled { get; set; }
        public bool catering { get; set; }
        public bool pet { get; set; }
    }
}
