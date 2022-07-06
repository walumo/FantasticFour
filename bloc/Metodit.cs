using System;
using System.Collections.Generic;
using System.Text;
using FantasticFour.network;
using FantasticFour.models;
using System.Threading.Tasks;

namespace FantasticFour.bloc
{
    internal class Metodit
    {
        public static async Task Departure( string lähtöAsema, string määränpääAsema, DateTime lähtöPvm)
        {

            string url = "/live-trains/station/" + lähtöAsema + "/" + määränpääAsema + "?departure_date=" + lähtöPvm.ToString("yyyy-MM-dd");
            //string url2 = "/live-trains/station/" + määränpääAsema + "/" + lähtöAsema + "?departure_date=" + lähtöPvm.ToString("yyyy-MM-dd");

            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            trains.ForEach((train) => Console.WriteLine(train.trainNumber));

            //Console.WriteLine("TESTI!!!!!");

            //var trains2 = await json.GetDataAsync<List<Train>>(url2);
            //trains2.ForEach((train1) => Console.WriteLine(train1.trainType));

        }

        //overload
        public static async Task Departure( string lähtöAsema, string määränpääAsema)
        {

            string url = "/live-trains/station/" + lähtöAsema + "/" + määränpääAsema;
            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            trains.ForEach((train) => Console.WriteLine(train.trainNumber));

        }

        public static async Task Arrivals(string määränpääAsema)
        {
            string url = "/live-trains/station/" + määränpääAsema + "?minutes_before_departure=0&minutes_after_departure=0&minutes_before_arrival=20&minutes_after_arrival=20";
            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            trains.ForEach((train) => Console.WriteLine(train.trainNumber));
        }

    }
}
