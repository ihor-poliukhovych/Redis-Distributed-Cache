namespace RedisDistributedCache.Models
{
    public class MyComplexValue
    {           
        public int Id { get; set; }
        public string Name { get; set; }
        public MyValue[] Values { get; set; }
        
        public MyComplexValue()
        {
            
        }

        public MyComplexValue(int id, string name, MyValue[] values)
        {
            Id = id;
            Name = name;
            Values = values;
        }
    }
}