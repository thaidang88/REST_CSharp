using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;


namespace Catalog.Repositories
{
    public class MongoRepositoriesAsync : IMemRepositoriesAsync
    {
        private readonly IMongoCollection<Item> itemsCollection;
        private const string databaseName="catalog";
        private const string collectionName="items";

        private readonly FilterDefinitionBuilder<Item> filterDefinitionBuilder= Builders<Item>.Filter;

        public MongoRepositoriesAsync(IMongoClient mongoClient)
        {
            IMongoDatabase database= mongoClient.GetDatabase(databaseName);
            itemsCollection= database.GetCollection<Item>(collectionName);
        }
        public async Task CreateItemAsync(Item item)
        {
            // throw new NotImplementedException();
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            // throw new NotImplementedException();
            var filter= filterDefinitionBuilder.Eq(item =>item.Id,id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            // throw new NotImplementedException();
            // return itemsCollection.FindAsync(new BsonDocument()).ToList();
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task RemoveItemAsync(Guid id)
        {
            // throw new NotImplementedException();
            var filter= filterDefinitionBuilder.Eq(item =>item.Id,id);
            await itemsCollection.DeleteOneAsync(filter) ;
        }

        public async Task UpdateItemAsync(Item item)
        {
            // throw new NotImplementedException();
            var filter= filterDefinitionBuilder.Eq(item =>item.Id,item.Id); 
            await itemsCollection.ReplaceOneAsync(filter,item)          ;
        }

        // public Task RemoveItem(Guid id)
        // {
        //     throw new NotImplementedException();
        // }
    }
}