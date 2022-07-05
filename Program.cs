using System;
using FantasticFour.models;
using System.Collections.Generic;
using System.Threading.Tasks;
using FantasticFour.network;

namespace FantasticFour
{
    static class Program
    {
        static async Task Main(string[] args)
        {

            NetworkConnection.InitializeClient();

            JsonClient client = new JsonClient();

            var trains = await client.GetDataAsync<List<Train>>("/trains/latest/5");

        }
    }
}
