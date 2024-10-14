using LotteryApp1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LotteryApp1.Services
{
    public interface ILotteryDrawService
    {
        Task<LotteryDrawResponse> GetLotteryDraws(LotteryDrawRequestType requestType, bool cached = false);
    }
}
