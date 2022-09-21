using System.Collections.Generic;
using System;
using Catalog.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Repositories
{
    
    public class InMemRepositoriesAsync : IMemRepositoriesAsync
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Romeo", Price = 10, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Juliet", Price = 10, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "dasd", Price = 10, CreatedDate = DateTimeOffset.UtcNow }
        };

        // public IEnumerable<Item> GetItems()
        // {
        //     return items;
        // }

        // public Item GetItem(Guid id)
        // {
        //     return items.Where(item => item.Id==id).SingleOrDefault();
        //     // foreach (Item i in items)
        //     // {
        //     //     if (i.Id == id)
        //     //     {
        //     //         return i;
        //     //     }
        //     // }
        //     // return null;
        // }

        // public void CreateItem(Item item)
        // {
        //     // throw new NotImplementedException();
        //     items.Add(item);
        // }

        // public void UpdateItem(Item item)
        // {
        //     // throw new NotImplementedException();
        //     var index = items.FindIndex(existing_item => existing_item.Id==item.Id);
        //     items[index]=item;
        // }

        // public void RemoveItem(Guid id)
        // {
        //     // throw new NotImplementedException();
        //     var index = items.FindIndex(existing_item => existing_item.Id==id);
        //     items.RemoveAt(index);
        // }

        public async Task<Item> GetItemAsync(Guid id)
        {
            // throw new NotImplementedException();
            var result= items.Where(item => item.Id==id).SingleOrDefault();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            // throw new NotImplementedException();
            return await Task.FromResult(items);
        }

        public async Task CreateItemAsync(Item item)
        {
            // throw new NotImplementedException();
            items.Add(item);
            await Task.CompletedTask;
        }

        public async Task UpdateItemAsync(Item item)
        { 
            // throw new NotImplementedException();
            var index = items.FindIndex(existing_item => existing_item.Id==item.Id);
            items[index]=item;
            await Task.CompletedTask;
        }

        public async Task RemoveItemAsync(Guid id)
        {
            // throw new NotImplementedException();
            var index = items.FindIndex(existing_item => existing_item.Id==id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}