## Lottery.Api
#### Tech Stack: 
1) Web API in Asp.Net Core 3.1 taking advantage of built-in dependency injection framework using Visual Studio 2019.
   1) Swagger UI is added for api testing. 
   2) SeriLog is used for logging.
   3) Unit tests done using XUnit and Moq.
   4) Console application for UI

#### How to run:

Download the source code from GIT:

1) After downloading source code from GIT, Open the solution from Visual Studio 2019 which will load web api project **Lottery.Api** and unit test project with .net Core 3.1 framework - build and run the api project from visual studio.
   It should launch Swagger UI on http://localhost:36101/swagger/index.html
2) Console application can be run after Lottery.Api is launched to see the generated lottery numbers.

Note: You can change the LotteryApiEndPoint query parameter in app.config of Console application to change bonus ball count from 6 to 7 or any other number upto the maximum acceptable value of 48