using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FantasticFour.network;
using FantasticFour.models;
using System.Threading.Tasks;
using System.Text;

namespace FantasticFour.bloc
{
    public static class Search
    {
        public static async Task<string> GetShortStationName(string str)
        {
            var list = await new JsonClient().GetDataAsync<List<Station>>("/metadata/stations");
            try
            {
                return (from station in list where string.Equals(station.stationName, str, StringComparison.OrdinalIgnoreCase) select station.stationShortCode).First();
            }
            catch (Exception ex)
            {
                return (from station in list where Regex.IsMatch(station.stationName, str, RegexOptions.IgnoreCase) select station.stationShortCode).First();
            }
        }
        public static async Task<string> GetStationName()
        {
            List<Station> list = await new JsonClient().GetDataAsync<List<Station>>("/metadata/stations");
            string searchString = "";
            while (true)
            {
                List<Station> sIndex = new List<Station>();

                Console.Write("Search: "+searchString);
                ConsoleKeyInfo letter = Console.ReadKey();
                Console.Clear();
                
                if(!Regex.IsMatch(letter.KeyChar.ToString(), "[a-z]", RegexOptions.IgnoreCase)
                    && letter.Key != ConsoleKey.Backspace 
                    && letter.Key != ConsoleKey.Enter
                    && letter.Key != ConsoleKey.Spacebar) continue;

                if (letter.Key != ConsoleKey.Backspace && letter.Key != ConsoleKey.Enter) searchString += letter.KeyChar.ToString();
                else if (letter.Key == ConsoleKey.Backspace && searchString.Count() > 0) searchString = searchString.Remove(searchString.Count() - 1, 1);
                else if (letter.Key == ConsoleKey.Enter && !String.IsNullOrWhiteSpace(searchString)) return await GetShortStationName(searchString);
                

                if (!String.IsNullOrWhiteSpace(searchString))
                {
                    IEnumerable<Station> parsedList = list.Where(station => Regex.IsMatch(station.stationName, searchString, RegexOptions.IgnoreCase));


                    foreach (Station item in parsedList)
                    {
                        if (item.stationName.StartsWith(searchString, StringComparison.OrdinalIgnoreCase))
                        {
                            sIndex.Add(item);
                        }
                    }

                    if (sIndex.Count > 0)
                    {
                        sIndex.ForEach((x) => Console.WriteLine(x.stationName + " " + x.stationShortCode));
                        Console.WriteLine(Environment.NewLine);
                    }
                    else
                    {
                        Console.WriteLine("Not found!");
                    }
                }
            }
        }
    }
}