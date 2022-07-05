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
        public static async Task Departure(DateTime lähtöPvm, string lähtöAsema, string määränpääAsema)
        {
            const string url = "/live-trains/station/HKI/TPE";
            var json = new JsonClient();

            var trains = await json.GetDataAsync<List<Train>>(url);
            Console.WriteLine(trains[0].trainType);
        }
    }
}
