using cw15.DTOs;
using cw15.Entities;
using cw15.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace cw15.Infrastructure.DataAccess.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public List<Product> SearchProduct(ProductSearchDto filter)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (filter.MinPrice.HasValue)
                query = query.Where(p => p.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);

            if (!string.IsNullOrEmpty(filter.Color))
                query = query.Where(p => p.Color == filter.Color);

            if (!string.IsNullOrEmpty(filter.Brand))
                query = query.Where(p => p.Brand == filter.Brand);

            if (filter.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == filter.CategoryId.Value);

            if (!string.IsNullOrEmpty(filter.CategoryName))
                query = query.Where(p => p.Category != null && p.Category.Name.Contains(filter.CategoryName));

            query = filter.SortBy?.ToLower() switch
            {
                "price" => filter.IsDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                "name" => filter.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "stock" => filter.IsDescending ? query.OrderByDescending(p => p.Stock) : query.OrderBy(p => p.Stock),
                _ => query
            };

            return query.ToList();
        }
    }

}
