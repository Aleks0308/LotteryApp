using System.Collections.Generic;

namespace LotteryApp1.Models
{
    public class LotteryDrawResponse
    {
        public IEnumerable<LotteryDraw> draws = new LotteryDraw[0];
    }
}
