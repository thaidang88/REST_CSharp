using MongoDB.Driver;
using MongoDB.Bson;
using Play.Catalog.Service.Entities;
using System;
using System.Threading.Tasks;
using Play.Catalog.Common;
using System.Collections.Generic;
using Play.Catalog.Service.Settings;
namespace Play.Catalog.Service.Repository
{
    public class ItemsRepository:IItemRepository
    {
        private const String collections="items";
        private readonly IMongoCollection<Item> dbCollection;
        private readonly FilterDefinitionBuilder <Item> filterBuilder= Builders<Item>.Filter;

        public ItemsRepository()
        {
            var mongoClient= new MongoClient("mongodb://localhost::27017")    ;
            var database = mongoClient.GetDatabase("Catalog");
            

            // MongoDBSettings mongoDBSettings;
            // MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            // IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            dbCollection= database.GetCollection<Item> (collections);
        }

        // public async Task<IReadOnlyCollection<Item>> GetItemsAsync()
        // {
        //     return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        // }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            FilterDefinition<Item> filter= filterBuilder.Eq(entity=>entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateItemAsync(Item item)
        {
            // throw new NotImplementedException();
            await dbCollection.InsertOneAsync(item);
        }

        

        public async Task RemoveItemAsync(Guid id)
        {
            // throw new NotImplementedException();
            var filter= filterBuilder.Eq(item =>item.Id,id);
            await dbCollection.DeleteOneAsync(filter) ;
        }

        public async Task UpdateItemAsync(Item item)
        {
            // throw new NotImplementedException();
            var filter= filterBuilder.Eq(item =>item.Id,item.Id); 
            await dbCollection.ReplaceOneAsync(filter,item)          ;
        }

    }
}