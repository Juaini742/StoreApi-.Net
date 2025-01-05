﻿namespace BackendStore.Model
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Type { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }

        public Product()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
