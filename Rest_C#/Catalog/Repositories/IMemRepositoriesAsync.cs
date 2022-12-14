using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
public interface IMemRepositoriesAsync
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task CreateItemAsync(Item item);

        Task UpdateItemAsync(Item item);

        Task RemoveItemAsync(Guid id);
    }
}