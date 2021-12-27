using System;
using GraphQLProject.Attributes;
using HotChocolate;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQLProject.Models
{
    public class Category
    {
        [DoNotExpose]
        [BsonId]
        public Guid Id { get; set; }
        [GraphQLDescription($"Name of Category.")] 
        public string Name { get; set; }
        [DoNotExpose]
        public DateTime CreateAt => DateTime.UtcNow;
         
        
        public Category()
        {

        }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name.ToLowerInvariant();
        }
    }
}
