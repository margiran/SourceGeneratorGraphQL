using System;
using GraphQLProject.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQLProject.Models
{
    public class Test
    {
        [DoNotExpose]
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }
        [DoNotExpose]
        public DateTime CreateAt => DateTime.UtcNow;
         
        
        public Test()
        {

        }

        public Test(string name)
        {
            Id = Guid.NewGuid();
            Name = name.ToLowerInvariant();
        }
    }
}
