using LotteryApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotteryApp1.Services
{
    public class LotteryDrawMockDataService: ILotteryDrawMockDataService
    {
        public LotteryDrawResponse GetMockLotteryDrawData(LotteryDrawRequestType requestType)
        {
            switch (requestType)
            {
                case LotteryDrawRequestType.Latest:
                    return JsonConvert.DeserializeObject<LotteryDrawResponse>(GetMockLotteryDrawJson());
                case LotteryDrawRequestType.Random:
                    return GetRandomLotteryDrawResponse(3, 10);
                case LotteryDrawRequestType.LuckyDip:
                    return GetLuckyDipLotteryDrawResponse();

            }
            return new LotteryDrawResponse();
        }

        private static LotteryDrawResponse GetLuckyDipLotteryDrawResponse()
        {
            return new LotteryDrawResponse { draws = new List<LotteryDraw>() { GenerateRandomLotteryDraw() } };
        }

        public LotteryDrawResponse GetRandomLotteryDrawResponse(int minAmount, int maxAmount)
        {
            var random = new Random();
            var drawQuantity = random.Next(minAmount, maxAmount);

            return new LotteryDrawResponse
            {
                draws = Enumerable.Range(0, drawQuantity).Select(_ => GenerateRandomLotteryDraw()).ToArray()
            };
        }

        private static LotteryDraw GenerateRandomLotteryDraw()
        {
            var random = new Random();
            var ballNumbers = Enumerable.Range(1, 59).OrderBy(n => random.Next()).ToArray();

            return new LotteryDraw
            {
                Id = $"draw-{random.Next(1, 50000)}",
                DrawDate = DateTime.Now.AddDays(-random.Next(0, 365)),
                Number1 = ballNumbers.ElementAt(0),
                Number2 = ballNumbers.ElementAt(1),
                Number3 = ballNumbers.ElementAt(2),
                Number4 = ballNumbers.ElementAt(3),
                Number5 = ballNumbers.ElementAt(4),
                Number6 = ballNumbers.ElementAt(5),
                BonusBall = ballNumbers.ElementAt(6),
                TopPrize = random.Next(5, 200) * 1_000_000
            };
        }

        public string GetMockLotteryDrawJson()
        {
            return @"{
              ""draws"": [
                {
                  ""id"": ""draw-1"",
                  ""drawDate"": ""2023-05-15"",
                  ""number1"": ""2"",
                  ""number2"": ""16"",
                  ""number3"": ""23"",
                  ""number4"": ""44"",
                  ""number5"": ""47"",
                  ""number6"": ""52"",
                  ""bonus-ball"": ""14"",
                  ""topPrize"": 4000000000
                },
                {
                  ""id"": ""draw-2"",
                  ""drawDate"": ""2023-05-22"",
                  ""number1"": ""5"",
                  ""number2"": ""45"",
                  ""number3"": ""51"",
                  ""number4"": ""32"",
                  ""number5"": ""24"",
                  ""number6"": ""18"",
                  ""bonus-ball"": ""28"",
                  ""topPrize"": 6000000000
                },
                {
                  ""id"": ""draw-3"",
                  ""drawDate"": ""2023-05-29"",
                  ""number1"": ""34"",
                  ""number2"": ""21"",
                  ""number3"": ""4"",
                  ""number4"": ""58"",
                  ""number5"": ""1"",
                  ""number6"": ""15"",
                  ""bonus-ball"": ""51"",
                  ""topPrize"": 6000000000
                }
              ]
            }";
        }
    }
}
