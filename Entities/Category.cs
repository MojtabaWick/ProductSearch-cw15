using System.ComponentModel.DataAnnotations;

namespace cw15.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
