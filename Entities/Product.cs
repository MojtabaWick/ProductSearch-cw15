using System.ComponentModel.DataAnnotations;

namespace cw15.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [MaxLength(100)]
        public string Color { get; set; }
        [MaxLength(100)]
        public string Brand { get; set; }
        public int Stock { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }

    }
}
