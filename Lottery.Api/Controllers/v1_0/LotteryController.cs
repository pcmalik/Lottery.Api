using Lottery.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lottery.Api.Controllers.v1_0
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotteryController : ControllerBase
    {

        private readonly ILotteryService _LotteryService;

        /// <summary>
        /// Lottery ticket generator
        /// </summary>
        /// <param name="LotteryService"></param>
        public LotteryController(ILotteryService LotteryService)
        {
            _LotteryService = LotteryService;
        }


        /// <summary>
        /// This api will be responsible for Generating non-repeating set of lottery numbers based on given non zero ball counts
        /// <param name="ballCount">, example if count is 6 then the function will return non-repeating set of six lottery numbers</param>
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ActionResult), StatusCodes.Status400BadRequest)]
        public ActionResult GenerateLotteryNumbers(int ballCount)
        {

            if (ballCount <= 0)
                return new BadRequestObjectResult("Invalid Input: ballCount value must be greater than zero");

            return Ok(_LotteryService.GenerateLotteryNumbers(ballCount));
        }
    }
}