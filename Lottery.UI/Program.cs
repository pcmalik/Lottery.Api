using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lottery.UI
{
    internal static class Program
    {
        private const int MAX_BALL_COUNT = 49;

        private static async Task Main(string[] args)
        {
            await ProcessLottery();
        }

        private async static Task ProcessLottery()
        {
            var config = LoadConfig();

            if (config == null)
            {
                Log("Invalid config. Press any key to exit");
                Console.Read();
                return;
            }

            var httpResponse = MakeHttpGetRequest(config.LotteryApiEndPoint);
            var lotteryResponse = await httpResponse.GetContentAs<Contracts.Lottery>();

            if (lotteryResponse != null)
            {
                foreach (var number in lotteryResponse.GeneratedNumbers)
                {
                    if (number >= 1 && number <= 9)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    if (number >= 10 && number <= 19)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    if (number >= 20 && number <= 29)
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                    }
                    if (number >= 30 && number <= 39)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                    }
                    if (number >= 40 && number <= 49)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }

                    Console.WriteLine(number);
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;

            Log($"Process finished.");

            Log("Press any key to exit.");
            Console.Read();
        }

        /// <summary>
        /// Get the lottery end point form config
        /// </summary>
        /// <returns></returns>
        private static Config LoadConfig()
        {
            Log("Loading config...");

            Config config = null;
            AppSettingsReader settingsReader = new AppSettingsReader();
            var uriString = settingsReader.GetValue("LotteryApiEndPoint", typeof(string)).ToString();
            if (!Uri.TryCreate(uriString, UriKind.Absolute, out var uri))
            {
                Log($"Invalid config value for 'LotteryApiEndPoint'. Value must be a valid absolute URI. Found: '{uriString}'");
                return null;
            }

            string queryString = new System.Uri(uriString).Query;
            var queryDictionary = System.Web.HttpUtility.ParseQueryString(queryString);
            var ballCount = Convert.ToInt16(queryDictionary["ballCount"]);

            if (ballCount <= 0 || ballCount >= MAX_BALL_COUNT)
            {
                Log($"Invalid config value for 'LotteryApiEndPoint'. ballCount query parameter must be set between 1 and 48. Found: '{uriString}'. Please update ballCount with valid value and then retry");
                return null;
            }

            config = new Config()
            {
                LotteryApiEndPoint = uri,
            };

            return config;
        }

        /// <summary>
        /// Call to handle http get request
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        private static HttpResponseMessage MakeHttpGetRequest(Uri requestUri)
        {
            Log($"Making HTTP request to {requestUri}");

            try
            {
                var httpClient = new HttpClient();
                var httpResponse = httpClient.GetAsync(requestUri).Result;
                if (!httpResponse.IsSuccessStatusCode)
                {
                    Log("----- Start of HTTP Response -----");
                    Log($"ERROR: Failed! HTTP status code: {httpResponse.StatusCode}");
                    Log("----- End of HTTP Response -----");
                    return null;
                }

                return httpResponse;
            }
            catch (Exception ex)
            {
                Log("ERROR: " + ex.Message);
            }

            return null;
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}