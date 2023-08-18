using Catalog.Api.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Api.Data
{
    public class Catalogcontext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; set; }
        public Catalogcontext(IConfiguration configuration)
        {
            var Client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = Client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }
        
    }
}
