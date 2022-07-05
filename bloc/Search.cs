using FantasticFour.models;
using FantasticFour.network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FantasticFour.bloc
{
    public static class Search
    {
        public static string GetShortStationName(string str, List<Station> list)
        {
            try
            {
                return (from station in list where string.Equals(station.stationName, str, StringComparison.OrdinalIgnoreCase) select station.stationShortCode).First();
            }
            catch (Exception e1)
            {
                File.AppendAllText(Environment.CurrentDirectory + "errorLog.txt", DateTime.Now + "| error converting station name: " + e1.Message + "\n");
                try
                {
                    return (from station in list where Regex.IsMatch(station.stationName, str, RegexOptions.IgnoreCase) select station.stationShortCode).First();
                }
                catch (Exception e2)
                {
                    return "HKI";
                }
            }
        }
        public static async Task<string> GetStationName()
        {
            List<Station> list = await new JsonClient().GetDataAsync<List<Station>>("/metadata/stations");
            string searchString = "";
            while (true)
            {
                List<Station> sIndex = new List<Station>();

                Console.Write("Search: " + searchString);
                ConsoleKeyInfo letter = Console.ReadKey();
                Console.Clear();

                if (!Regex.IsMatch(letter.KeyChar.ToString(), "[a-ö]", RegexOptions.IgnoreCase)
                    && letter.Key != ConsoleKey.Backspace
                    && letter.Key != ConsoleKey.Enter
                    && letter.Key != ConsoleKey.Spacebar) continue;

                if (letter.Key != ConsoleKey.Backspace && letter.Key != ConsoleKey.Enter) searchString += letter.KeyChar.ToString();
                else if (letter.Key == ConsoleKey.Backspace && searchString.Count() > 0) searchString = searchString.Remove(searchString.Count() - 1, 1);
                else if (letter.Key == ConsoleKey.Enter && !String.IsNullOrWhiteSpace(searchString)) return await GetShortStationName(searchString, list);


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
                        Console.WriteLine("Not found!\n");
                    }
                }
            }
        }
    }
}