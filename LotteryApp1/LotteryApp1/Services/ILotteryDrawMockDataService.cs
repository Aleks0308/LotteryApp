using LotteryApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LotteryApp1.Services
{
    public interface ILotteryDrawMockDataService
    {
        LotteryDrawResponse GetMockLotteryDrawData(LotteryDrawRequestType requestType);
    }
}
