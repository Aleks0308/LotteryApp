using LotteryApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LotteryApp1.Services
{
    public class LotteryDrawDataStore : IDataStore<LotteryDraw>
    {
        readonly List<LotteryDraw> LotteryDraws = new List<LotteryDraw>();

        public async Task<bool> AddItemAsync(LotteryDraw Draw)
        {
            LotteryDraws.Add(Draw);

            return await Task.FromResult(true);
        }

        public async Task<bool> AddItemsAsync(IEnumerable<LotteryDraw> item)
        {
            LotteryDraws.AddRange(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(LotteryDraw Draw)
        {
            var oldDraw = LotteryDraws.Where((LotteryDraw arg) => arg.Id == Draw.Id).FirstOrDefault();

            if(oldDraw != null)
            {
                LotteryDraws.Remove(oldDraw);
                LotteryDraws.Add(Draw);

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldDraw = LotteryDraws.Where((LotteryDraw arg) => arg.Id == id).FirstOrDefault();
            if(oldDraw != null)
            {
                LotteryDraws.Remove(oldDraw);
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        public async Task<bool> DeleteItemsAsync()
        {
            LotteryDraws.Clear();

            return await Task.FromResult(true);
        }

        public async Task<LotteryDraw> GetItemAsync(string id)
        {
            return await Task.FromResult(LotteryDraws.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<LotteryDraw>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(LotteryDraws);
        }
    }
}