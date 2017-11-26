using System;

namespace RedisDistributedCache.Models
{
    public class MyValue
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public MyValue()
        {
            
        }
        
        public MyValue(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}