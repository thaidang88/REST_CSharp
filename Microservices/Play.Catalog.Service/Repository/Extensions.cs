using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Play.Catalog.Services.Repository;
using Play.Catalog.Service.Settings;
using Play.Catalog.Common;
using Play.Catalog.Service.Entities;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace Play.Catalog.Service.Repository
{
    public static class Extensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));    
            

            services.AddSingleton(serviceProvider=>
            {
                var configuration= serviceProvider.GetService<IConfiguration>();
                var serviceSetting = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDBSettings= configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
                var mongoClient= new MongoClient(mongoDBSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSetting.ServiceName);
            });
            return services;
           
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services,string collectionName) where T:IEntity
        {
            services.AddSingleton<IRepository<T>>(serviceProvider=>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database,collectionName);
            });
            return services;
        }
    }
}