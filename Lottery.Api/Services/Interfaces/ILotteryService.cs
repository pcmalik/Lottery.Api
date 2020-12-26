using System.Collections.Generic;

namespace Lottery.Api.Services.Interfaces
{
    public interface ILotteryService
    {
        Contracts.Lottery GenerateLotteryNumbers(int ballCount);
    }
}
