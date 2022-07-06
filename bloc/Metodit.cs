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
        
        public static async Task Departure(Options options)
        {
            string url = "/live-trains/station/" + options.DepartureStation + "/" + options.DestinationStation + "?departure_date=" + options.Date.ToString("yyyy-MM-dd");

            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            Show.DepartingTrains(trains, options);


        }


        public static async Task Arrivals(Options options)
        {
            string url = "/live-trains/station/" + options.DestinationStation + "?minutes_before_departure=0&minutes_after_departure=0&minutes_before_arrival=20&minutes_after_arrival=20";
            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            Show.ArrivingTrains(trains, options);
        }
    }
}
