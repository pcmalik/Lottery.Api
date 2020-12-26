using Lottery.Api.Services;
using Lottery.Api.Services.Interfaces;
using FluentAssertions;
using System;
using Xunit;

namespace Lottery.Api.Test.Services
{
    public class LotteryServiceTests
    {
        private readonly ILotteryService _LotteryService;

        public LotteryServiceTests()
        {
            //Arrange
            _LotteryService = new LotteryService();
        }

        [Theory]
        [InlineData(6, 6)]
        [InlineData(7, 7)]
        public void Should_Generate_Valid_Lottery_Numbers_When_BallCount_Is_Given(int ballCount, int expectedLotteryNumbers)
        {
            //Act
            var lottery = _LotteryService.GenerateLotteryNumbers(ballCount);

            //Assert
            Assert.Equal(expectedLotteryNumbers, lottery.GeneratedNumbers.Count);
        }

        [Fact]
        public void Should_Throw_InvalidOperationException_When_Invalid_BallCount_Is_Given()
        {
            //Act
            int ballCount = 0;

            //Assert
            Assert.Throws<InvalidOperationException>(() => _LotteryService.GenerateLotteryNumbers(ballCount));
        }

    }
}
