using System;
using System.Collections.Generic;
using System.Text;
using FantasticFour.network;
using FantasticFour.models;
using System.Threading.Tasks;

// Metodit millä haetaan apista tiedot, joita pyydetään main programissa. 
// käytämme digitrafficin apia https://www.digitraffic.fi/rautatieliikenne/
// Metodit toimii asynkronisina.
// Departure metodi hakee apista junia, jotka ovat lähtemässä käyttäjän pyytämältä asemalta, määritetylle reitille.
// Arrivals metodi hakee apista junat, jotka ovat saapumassa käyttäjän pyytämälle asemalle.



namespace FantasticFour.bloc
{
    internal class Metodit
    {
        // Departure
        public static async Task Departure( string lähtöAsema, string määränpääAsema, DateTime lähtöPvm)
        {
            string url = "/live-trains/station/" + lähtöAsema + "/" + määränpääAsema + "?departure_date=" + lähtöPvm.ToString("yyyy-MM-dd");

            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            trains.ForEach((train) => Console.WriteLine(train.trainNumber));


        }

        // Arrivals
        public static async Task Arrivals(string määränpääAsema)
        {
            string url = "/live-trains/station/" + määränpääAsema + "?minutes_before_departure=0&minutes_after_departure=0&minutes_before_arrival=20&minutes_after_arrival=20";
            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            trains.ForEach((train) => Console.WriteLine(train.trainNumber));
        }
        // Features
        public static async Task Features(DateTime lähtöPvm, int trainNumber)
        {
            string url = "/compositions/" + lähtöPvm.ToString("yyyy-MM-dd") + "/" + trainNumber;
          
            var json = new JsonClient();

            var trains = await json.GetDataAsync<RootobjectFeatures>(url);

            Console.WriteLine(trains.journeySections[0].wagons[0].wagonType);
        }
    }
}
