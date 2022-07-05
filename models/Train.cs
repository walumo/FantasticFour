using System;

namespace FantasticFour.models
{

    public class Rootobject
    {
        public Train[] Property1 { get; set; }
    }

    public class Train
    {
        public int trainNumber { get; set; }
        public string departureDate { get; set; }
        public int operatorUICCode { get; set; }
        public string operatorShortCode { get; set; }
        public string trainType { get; set; }
        public string trainCategory { get; set; }
        public string commuterLineID { get; set; }
        public bool runningCurrently { get; set; }
        public bool cancelled { get; set; }
        public long version { get; set; }
        public string timetableType { get; set; }
        public DateTime timetableAcceptanceDate { get; set; }
        public Timetablerow[] timeTableRows { get; set; }
    }

    public class Timetablerow
    {
        public string stationShortCode { get; set; }
        public int stationUICCode { get; set; }
        public string countryCode { get; set; }
        public string type { get; set; }
        public bool trainStopping { get; set; }
        public bool commercialStop { get; set; }
        public string commercialTrack { get; set; }
        public bool cancelled { get; set; }
        public DateTime scheduledTime { get; set; }
        public DateTime actualTime { get; set; }
        public int differenceInMinutes { get; set; }
        public Caus[] causes { get; set; }
        public Trainready trainReady { get; set; }
        public DateTime liveEstimateTime { get; set; }
        public string estimateSource { get; set; }
    }

    public class Trainready
    {
        public string source { get; set; }
        public bool accepted { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class Caus
    {
        public string categoryCode { get; set; }
        public string detailedCategoryCode { get; set; }
        public string thirdCategoryCode { get; set; }
        public int detailedCategoryCodeId { get; set; }
        public int categoryCodeId { get; set; }
        public int thirdCategoryCodeId { get; set; }
    }

}