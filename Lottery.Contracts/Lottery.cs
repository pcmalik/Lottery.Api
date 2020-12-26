using System.Collections.Generic;

namespace Lottery.Contracts
{
    public class Lottery
    {
        /// <summary>
        /// Randomly generated lottery numbers
        /// </summary>
        public IList<int> GeneratedNumbers { get; set; }
    }
}
