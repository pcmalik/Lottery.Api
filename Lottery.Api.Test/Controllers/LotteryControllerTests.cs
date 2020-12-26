using Lottery.Api.Controllers.v1_0;
using Lottery.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace Lottery.Api.Test.Controllers
{
    public class LotteryControllerTests
    {

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        public void Should_Return_200_OK_When_Valid_BallCounts_Are_Given(int ballCount)
        {
            //Arrange
            var LotteryService = new LotteryService();
            var controller = new LotteryController(LotteryService);

            //Act
            var result = controller.GenerateLotteryNumbers(ballCount);

            //Assert            
            ((ObjectResult)result).StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void Should_Return_400_BadRequest_When_BallCount_Is_Zero()
        {
            //Arrange
            int ballCount = 0;
            var LotteryService = new LotteryService();
            var controller = new LotteryController(LotteryService);

            //Act
            var result = controller.GenerateLotteryNumbers(ballCount);

            //Assert            
            ((ObjectResult)result).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

    }
}
