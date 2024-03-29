﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GraphQLProject.Mongo
{
   public class MongoSeeder: IDatabaseSeeder
    {
        protected readonly IMongoDatabase Database;

        public MongoSeeder(IMongoDatabase database)
        {
            Database = database;
        }
        public async Task SeedAsync()
        {
            var collectikonsCursor = await Database.ListCollectionsAsync();
            var collections =await collectikonsCursor.ToListAsync();

            if (collections.Any())
            {
                return;
            }

            await CustomSeedAsync();
        }

        protected virtual async Task CustomSeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
