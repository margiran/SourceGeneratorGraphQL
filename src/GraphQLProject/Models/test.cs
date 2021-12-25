using System;
using MongoDB.Bson.Serialization.Attributes;

namespace GraphQLProject.Models
{
    public class Test
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Name { get; set; }

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
