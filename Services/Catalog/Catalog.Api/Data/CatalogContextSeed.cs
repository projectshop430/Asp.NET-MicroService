using Catalog.Api.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Catalog.Api.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if(!existProduct)
            {
                productCollection.InsertManyAsync(GetSeedData());
            }
        }

        private static IEnumerable<Product> GetSeedData()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Iphone",
                    Summary = "Some text",
                    Description = "Description",
                    ImageFile = "img.png",
                    Price = 200,
                    Category = "SmartPhones"
                },
                new Product()
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "samsung",
                    Summary = "Some text",
                    Description = "Description",
                    ImageFile = "img.png",
                    Price = 200,
                    Category = "SmartPhones"
                }

            };
        }
    }
}
