using Newtonsoft.Json;
using System;

namespace LotteryApp1.Models
{
    public class LotteryDraw
    {
        
        public string Id { get; set; }
        public DateTime? DrawDate { get; set; }

        public int? Number1 { get; set; }
        public int? Number2 { get; set; }
        public int? Number3 { get; set; }
        public int? Number4 { get; set; }
        public int? Number5 { get; set; }
        public int? Number6 { get; set; }

        [JsonProperty("bonus-ball")]
        public int? BonusBall { get; set; }
        public double? TopPrize { get; set; }

    }
}