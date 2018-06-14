﻿namespace Backend.WebApi.Models
{
    public class Component : BaseEntity
    {
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public ComponentType ComponentType { get; set; }
        public double Price { get; set; }
    }
}
