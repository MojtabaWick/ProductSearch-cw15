using cw15.DTOs;
using cw15.Entities;
using cw15.Infrastructure.DataAccess.Repositories;

namespace cw15.Services
{
    public class ProductService
    {
        ProductRepository _repo = new ProductRepository();
        public List<Product> SearchProduct(ProductSearchDto filter)
        {
            return _repo.SearchProduct(filter);
        }
    }
}
