﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
            string url = "/live-trains/station/" + options.DestinationStation + "?arriving_trains=25&arrived_trains=0&departing_trains=0&departed_trains=0&train_categories=Commuter,Long-distance";
            
            var json = new JsonClient();
            var trains = await json.GetDataAsync<List<Train>>(url);
            Show.ArrivingTrains(trains, options);
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
