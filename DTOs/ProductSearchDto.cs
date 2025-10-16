namespace cw15.DTOs
{
    public class ProductSearchDto
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public string? SortBy { get; set; } 
        public bool IsDescending { get; set; } = false;

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
