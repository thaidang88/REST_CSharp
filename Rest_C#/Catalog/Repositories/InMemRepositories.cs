using System.Collections.Generic;
using System;
using Catalog.Entities;
using System.Linq;

namespace Catalog.Repositories
{
    
    public class InMemRepositories : IMemRepositories
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Romeo", Price = 10, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Juliet", Price = 10, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "dasd", Price = 10, CreatedDate = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id==id).SingleOrDefault();
            // foreach (Item i in items)
            // {
            //     if (i.Id == id)
            //     {
            //         return i;
            //     }
            // }
            // return null;
        }

        public void CreateItem(Item item)
        {
            // throw new NotImplementedException();
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            // throw new NotImplementedException();
            var index = items.FindIndex(existing_item => existing_item.Id==item.Id);
            items[index]=item;
        }

        public void RemoveItem(Guid id)
        {
            // throw new NotImplementedException();
            var index = items.FindIndex(existing_item => existing_item.Id==id);
            items.RemoveAt(index);
        }
    }
}