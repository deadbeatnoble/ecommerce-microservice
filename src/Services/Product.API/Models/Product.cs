using MongoDB.Bson.Serialization.Attributes;

namespace Product.API.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
    }
}
