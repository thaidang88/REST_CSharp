using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Driver;
using MongoDB.Bson;


namespace Catalog.Repositories
{
    public class MongoRepositories : IMemRepositories
    {
        private readonly IMongoCollection<Item> itemsCollection;
        private const string databaseName="catalog";
        private const string collectionName="items";

        private readonly FilterDefinitionBuilder<Item> filterDefinitionBuilder= Builders<Item>.Filter;

        public MongoRepositories(IMongoClient mongoClient)
        {
            IMongoDatabase database= mongoClient.GetDatabase(databaseName);
            itemsCollection= database.GetCollection<Item>(collectionName);
        }
        public void CreateItem(Item item)
        {
            // throw new NotImplementedException();
            itemsCollection.InsertOne(item);
        }

        public Item GetItem(Guid id)
        {
            // throw new NotImplementedException();
            var filter= filterDefinitionBuilder.Eq(item =>item.Id,id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            // throw new NotImplementedException();
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void RemoveItem(Guid id)
        {
            // throw new NotImplementedException();
            var filter= filterDefinitionBuilder.Eq(item =>item.Id,id);
            itemsCollection.DeleteOne(filter) ;
        }

        public void UpdateItem(Item item)
        {
            // throw new NotImplementedException();
            var filter= filterDefinitionBuilder.Eq(item =>item.Id,item.Id); 
            itemsCollection.ReplaceOne(filter,item)          ;
        }
    }
}