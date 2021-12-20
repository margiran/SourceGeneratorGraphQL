﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQLProject.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime CreateAt { get; set; }
        
        
        public Category()
        {

        }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name.ToLowerInvariant();
            CreateAt=DateTime.UtcNow;
        }
    }
}
