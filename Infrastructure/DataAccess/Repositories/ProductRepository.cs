using cw15.DTOs;
using cw15.Entities;
using cw15.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;

namespace cw15.Infrastructure.DataAccess.Repositories
{
    public class ProductRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public (List<Product> Items, int TotalCount) SearchProduct(ProductSearchDto filter)
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

            // --- Sorting ---
            if (filter.Sort != null && filter.Sort.Count > 0)
            {
                IOrderedQueryable<Product>? orderedQuery = null;
                for (int i = 0; i < filter.Sort.Count; i++)
                {
                    var sortItem = filter.Sort[i];
                    if (i == 0)
                    {
                        orderedQuery = sortItem.SortBy switch
                        {
                            "price" => sortItem.IsDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price),
                            "name" => sortItem.IsDescending ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                            "stock" => sortItem.IsDescending ? query.OrderByDescending(p => p.Stock) : query.OrderBy(p => p.Stock),
                            _ => query.OrderBy(p => p.Id)
                        };
                    }
                    else
                    {
                        orderedQuery = sortItem.SortBy switch
                        {
                            "price" => sortItem.IsDescending ? orderedQuery.ThenByDescending(p => p.Price) : orderedQuery.ThenBy(p => p.Price),
                            "name" => sortItem.IsDescending ? orderedQuery.ThenByDescending(p => p.Name) : orderedQuery.ThenBy(p => p.Name),
                            "stock" => sortItem.IsDescending ? orderedQuery.ThenByDescending(p => p.Stock) : orderedQuery.ThenBy(p => p.Stock),
                            _ => orderedQuery
                        };
                    }
                }

                query = orderedQuery!;
            }

            var totalCount = query.Count();

            var items = query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return (items, totalCount);
        }


    }

}
