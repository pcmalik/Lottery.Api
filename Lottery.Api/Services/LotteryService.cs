using Lottery.Api.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Lottery.Api.Services
{
    /// <summary>
    /// Lottery number generator service
    /// </summary>
    public class LotteryService : ILotteryService
    {
        private const int min = 1;
        private const int max = 49;

        /// <summary>
        /// This api will be responsible for Generating non-repeating set of lottery numbers based on given non zero ball counts
        /// </summary>
        /// <param name="ballCount">, example if count is 6 then the function will return non-repeating set of six lottery numbers</param>
        /// <returns></returns>
        public Contracts.Lottery GenerateLotteryNumbers(int ballCount)
        {
            // validate
            if (ballCount <= 0 || ballCount >= max)
            {
                throw new InvalidOperationException();
            }

            // using HashSet to avoid having duplicates.
            var numbers = new HashSet<int>();
            var Random = new Random();
            while (numbers.Count < ballCount)
            {
                numbers.Add(Random.Next(min, max));
            }

            // add them in a list to make use of LINQ sort functionality
            var generatedNumbers = new List<int>();
            generatedNumbers.AddRange(numbers);

            //sort the numbers in ascending order
            generatedNumbers.Sort();

            return new Contracts.Lottery() { GeneratedNumbers = generatedNumbers };
        }

    }
}
