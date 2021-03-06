﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using MovieCollection.OpenMovieDatabase;

namespace Demo
{
    internal class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
        private static readonly HttpClient _httpClient = new HttpClient();

        private static OpenMovieDatabaseConfiguration _configuration;
        private static OpenMovieDatabaseService _service;

        private static async Task Main()
        {
            // Initialize Configuration and OpenMovieDatabase Service
            _configuration = new OpenMovieDatabaseConfiguration("your-api-key-here");
            _service = new OpenMovieDatabaseService(_httpClient, _configuration);

            Console.WriteLine("- Searching for Interstellar...\n");
            await GetSingleMovieDemoAsync("interstellar");

            Console.WriteLine("\n******************************\n");

            Console.WriteLine("- Searching for Three Colors...\n");
            await GetMoviesDemoAsync("three colors");

            // Wait for user to exit
            Console.ReadKey();
        }

        private static async Task GetSingleMovieDemoAsync(string query)
        {
            var item = await _service.SearchMovieAsync(query);

            Console.WriteLine("Title: {0}", item.Title);
            Console.WriteLine("Year: {0}", item.Year);
            Console.WriteLine("Type: {0}", item.Type);
            Console.WriteLine("ImdbId: {0}", item.ImdbId);
            Console.WriteLine("ImdbRating: {0}", item.ImdbRating);
            Console.WriteLine("ImdbVotes: {0}", item.ImdbVotes);
            Console.WriteLine("Metascore: {0}", item.Metascore);
        }

        private static async Task GetMoviesDemoAsync(string query)
        {
            var items = await _service.SearchMoviesAsync(query);

            foreach (var item in items.Items)
            {
                Console.WriteLine("Title: {0}", item.Title);
                Console.WriteLine("Year: {0}", item.Year);
                Console.WriteLine("Type: {0}", item.Type);
                Console.WriteLine("ImdbId: {0}", item.ImdbId);
                Console.WriteLine("******************************");
            }
        }
    }
}
