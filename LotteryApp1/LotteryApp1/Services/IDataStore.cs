using System.Collections.Generic;
using System.Threading.Tasks;

namespace LotteryApp1.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> AddItemsAsync(IEnumerable<T> item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<bool> DeleteItemsAsync();
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
