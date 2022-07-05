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
            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            trains.ForEach((train) => Console.WriteLine(train.trainNumber));

        }

        //overload
        public static async Task Departure( string lähtöAsema, string määränpääAsema)
        {

            string url = "/live-trains/station/" + lähtöAsema + "/" + määränpääAsema;
            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            trains.ForEach((train) => Console.WriteLine(train.trainNumber));

        }



    }
}
